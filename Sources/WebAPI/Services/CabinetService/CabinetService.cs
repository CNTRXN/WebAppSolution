using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using ModelLib.Model;
using ModelLib.DTO;
using WebAPI.Other;

namespace WebAPI.Services.CabinetService
{
    public class CabinetService(DB_Context context) : ICabinetService
    {
        public async Task<Cabinet?> AddCabinet(NewCabinetDTO newCabinet)
        {
            var cabinets = await context.Cabinets.ToListAsync();

            if (EqualEntity.CabinetIsExist(cabinets, newCabinet))
                return null;

            var addedCabinet = new Cabinet()
            {
                Num = newCabinet.Num,
                PlanNum = newCabinet.PlanNum,
                Floor = newCabinet.Floor,
                //Group = newCabinet.Group,
                Height = newCabinet.Height,
                Length = newCabinet.Length,
                Width = newCabinet.Width,
                ResponsiblePersonId = newCabinet.ResponsiblePersonId
            };

            await context.AddAsync(addedCabinet);
            await context.SaveChangesAsync();

            return addedCabinet;
        }

        public async Task<int> AddEquipmentsToCabinet(int cabId, IEnumerable<int> equipmentsIds) 
        {
            //Проверка на существование кабинета
            var cabinetIsExist = await context.Cabinets.AnyAsync(c => c.Id == cabId);

            if (!cabinetIsExist)
                return 0;

            List<CabinetEquipment> equipmentToAdd = [];

            //Проверка на существование оборудования
            foreach (var equipmentId in equipmentsIds) 
            {
                var equipment = await context.Equipments
                    .FirstOrDefaultAsync(e => e.Id == equipmentId);

                if (equipment != null) 
                {
                    //Проверка: не привязано ли оборудование к какому-нибудь кабинету
                    var equipmentInOtherCabinet = await context.CabinetEquipments
                        .AnyAsync(ce => ce.EquipmentId == equipment.Id);

                    if (!equipmentInOtherCabinet)
                    {
                        equipmentToAdd.Add(new CabinetEquipment() 
                        {
                            CabinetId = cabId,
                            EquipmentId = equipment.Id
                        });
                    }
                }
            }

            if (equipmentToAdd.Count == 0)
                return 0;

            await context.CabinetEquipments.AddRangeAsync(equipmentToAdd);
            await context.SaveChangesAsync();

            return equipmentToAdd.Count;
        }

        public async Task<bool> DeleteCabinet(int id)
        {
            var cabinet = await context.Cabinets
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cabinet == null)
                return false;

            context.Cabinets.Remove(cabinet);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<CabinetDTO?> GetCabinet(int id)
        {
            var cabinet = await context.Cabinets
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cabinet == null)
                return null;

            CabinetDTO retCab = new()
            {
                Id = cabinet.Id,
                Floor = cabinet.Floor,
                //Group = cabinet.Group,
                Height = cabinet.Height,
                Length = cabinet.Length,
                Num = cabinet.Num,
                PlanNum = cabinet.PlanNum,
                Width = cabinet.Width,
                ResponsiblePerson = await context.Users
                    .Where(u => u.Id == cabinet.ResponsiblePersonId)
                    .Select(u => new UserDTO()
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Patronymic = u.Patronymic,
                        Surname = u.Surname,
                        Birthday = u.Birthday,
                        Permission = context.Permissions
                            .Where(p => p.Id == u.PermissionId).First(),
                    }).FirstOrDefaultAsync()
            };

            return retCab;
        }

        public async Task<IEnumerable<EquipmentDTO>> GetCabinetEquipments(int cabId)
        {
            var cabinetsEquipments = await context.CabinetEquipments
                .Where(ce => ce.CabinetId == cabId)
                .ToListAsync();

            List<EquipmentDTO> equipments = [];
            foreach (var cabEquip in cabinetsEquipments)
            {
                var foundedEquipment = await context.Equipments
                    .Where(e => e.Id == cabEquip.EquipmentId)
                    .FirstOrDefaultAsync();

                if (foundedEquipment is Equipment equipment)
                {
                    equipments.Add(new EquipmentDTO
                    {
                        Id = equipment.Id,
                        Name = equipment.Name,
                        //Count = equipment.Count,
                        Description = equipment.Description,
                        InventoryNumber = equipment.InventoryNumber,
                        EquipmentType = await context.EquipmentTypes.Where(et => et.Id == equipment.TypeId).FirstAsync()
                    });
                }
            }

            return equipments;
        }

        public async Task<IEnumerable<CabinetDTO>> GetCabinets()
        {
            var cabs = await context.Cabinets.ToListAsync();

            List<CabinetDTO> result = [];

            foreach (var cab in cabs)
            {
                var convCab = new CabinetDTO()
                {
                    Id = cab.Id,
                    Num = cab.Num,
                    Floor = cab.Floor,
                    //Group = cab.Group,
                    Length = cab.Length,
                    Height = cab.Height,
                    Width = cab.Width,
                    PlanNum = cab.PlanNum
                };

                var resposiblePerson = await context.Users
                    .Where(u => u.Id == cab.ResponsiblePersonId)
                    .Select(u => new UserDTO()
                    {
                        Id = u.Id,
                        Birthday = u.Birthday,
                        Name = u.Name,
                        Patronymic = u.Patronymic,
                        Permission = context.Permissions
                            .Where(p => p.Id == u.PermissionId)
                            .First(),
                        Surname = u.Surname
                    })
                    .FirstOrDefaultAsync();

                if (resposiblePerson is UserDTO _resposiblePerson)
                    convCab.ResponsiblePerson = _resposiblePerson;

                result.Add(convCab);
            }

            return result;
        }


        public async Task<bool> UpdateCabinet(int id, NewCabinetDTO updateCabinet) 
        {
            var foundedCabinet = await context.Cabinets.FirstOrDefaultAsync(c => c.Id == id);

            if (foundedCabinet == null)
                return false;

            foundedCabinet.Floor = updateCabinet.Floor;
            foundedCabinet.Length = updateCabinet.Length;
            foundedCabinet.Height = updateCabinet.Height;
            foundedCabinet.Width = updateCabinet.Width;

            if (updateCabinet.ResponsiblePersonId > 0)
            {
                var responsiblePersonIsExist = await context.Users.AnyAsync(u => u.Id == updateCabinet.ResponsiblePersonId);

                if (!responsiblePersonIsExist)
                    return false;

                foundedCabinet.ResponsiblePersonId = updateCabinet.ResponsiblePersonId;
            }

            foundedCabinet.PlanNum = updateCabinet.PlanNum;
            foundedCabinet.Num = updateCabinet.Num;

            context.Cabinets.Update(foundedCabinet);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CabinetDTO>?> GetCabinetByUser(int userId) 
        {
            if (userId == 0)
                return null;

            var foundedCabinets = await context.Cabinets.Where(c => c.ResponsiblePersonId == userId).ToListAsync();

            if (foundedCabinets == null)
                return null;

            List<CabinetDTO> cabinets = [];

            if (foundedCabinets.Count == 0)
                return cabinets;

            foundedCabinets.ForEach(elem => 
            {
                var responsiblePerson = context.Users
                    .Where(u => u.Id == elem.ResponsiblePersonId)
                    .Select(u => new UserDTO()
                    {
                        Id = u.Id,
                        Birthday = u.Birthday,
                        Name = u.Name,
                        Patronymic = u.Patronymic,
                        Surname = u.Surname,
                        Permission = context.Permissions.First(p => p.Id == u.Id)
                    })
                    .FirstOrDefault();

                cabinets.Add(new()
                {
                    Id = elem.Id,
                    Floor = elem.Floor,
                    Height = elem.Height,
                    Length = elem.Length,
                    Num = elem.Num,
                    PlanNum = elem.PlanNum,
                    Width = elem.Width,
                    ResponsiblePerson = responsiblePerson
                });
            });

            return cabinets;
        }
    }
}
