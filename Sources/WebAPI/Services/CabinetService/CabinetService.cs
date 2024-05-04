using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
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
                Group = newCabinet.Group,
                Height = newCabinet.Height,
                Length = newCabinet.Length,
                Width = newCabinet.Width,
                ResponsiblePersonId = newCabinet.ResponsiblePersonId
            };

            await context.AddAsync(addedCabinet);
            await context.SaveChangesAsync();

            return addedCabinet;
        }

        public async Task<int?> AddEquipmentsToCabinet(int cabId, IEnumerable<AddEquipToCabDTO> equipments)
        {
            var cabinet = await context.Cabinets
                .Where(c => c.Id == cabId)
                .FirstOrDefaultAsync();

            int addedRows = 0;
            List<CabEquipment> addedEquipment = [];

            if (cabinet != null)
            {
                //Проверка на существование оборудования
                foreach (var equipment in equipments)
                {
                    var equip = await context.Equipments
                        .Where(e => e.Id == equipment.EquipmentId)
                        .FirstOrDefaultAsync();

                    if (equip != null && equip.Count > 0)
                    {
                        int totalCount = 0;
                        switch (equip.Count - equipment.AddedEquipmentCount)
                        {
                            case <= 0:
                                totalCount = equip.Count;
                                equip.Count = 0;
                                break;
                            case > 0:
                                totalCount = equipment.AddedEquipmentCount;
                                equip.Count -= equipment.AddedEquipmentCount;
                                break;
                        }

                        addedEquipment.Add(new CabEquipment()
                        {
                            Id = equip.Id,
                            CabId = cabinet.Id,
                            EquipId = equip.Id,
                            Count = totalCount
                        });

                        context.Equipments.Update(equip);
                        await context.SaveChangesAsync();

                        addedRows++;
                    }
                }

                //Добавление оборудование к кабинету
                foreach (var equipment in addedEquipment)
                {
                    var equipInCab = await context.CabEquipments
                        .Where(eq => eq.EquipId == equipment.Id)
                        .FirstOrDefaultAsync();

                    if (equipInCab == null)
                    {
                        //Если оборудования нет в кабинет, то оно добавляется
                        await context.CabEquipments.AddAsync(new CabEquipment()
                        {
                            CabId = cabinet.Id,
                            EquipId = equipment.Id,
                            Count = equipment.Count
                        });
                    }
                    else
                    {
                        //Если оборудование есть в кабинете, то прибавляется количество добавляемого
                        equipInCab.Count += equipment.Count;

                        context.CabEquipments.Update(equipInCab);
                    }
                    await context.SaveChangesAsync();
                }

                //await context.CabEquipments.AddRangeAsync(addedEquipment);
                await context.SaveChangesAsync();
            }

            return addedRows;
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
                Group = cabinet.Group,
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
                        PermissionName = context.Permissions
                            .Where(p => p.Id == u.PermissionId)
                            .Select(p => p.Name).First(),
                    }).FirstOrDefaultAsync()
            };

            return retCab;
        }

        public async Task<IEnumerable<EquipmentDTO>> GetCabinetEquipments(int cabId)
        {
            var cabinetsEquipments = await context.CabEquipments
                .Where(ce => ce.CabId == cabId)
                .ToListAsync();

            List<EquipmentDTO> equipments = [];
            foreach (var cabEquip in cabinetsEquipments)
            {
                var foundedEquipment = await context.Equipments
                    .Where(e => e.Id == cabEquip.EquipId)
                    .FirstOrDefaultAsync();

                if (foundedEquipment is Equipment equipment)
                {
                    equipments.Add(new EquipmentDTO
                    {
                        Id = equipment.Id,
                        Name = equipment.Name,
                        Count = equipment.Count,
                        Description = equipment.Description,
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
                    Group = cab.Group,
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
                        PermissionName = u.Patronymic ?? string.Empty,
                        Surname = u.Surname
                    })
                    .FirstOrDefaultAsync();

                if (resposiblePerson is UserDTO _resposiblePerson)
                    convCab.ResponsiblePerson = _resposiblePerson;

                result.Add(convCab);
            }

            return result;
        }
    }
}
