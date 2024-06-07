using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using ModelLib.Model;
using ModelLib.DTO;

namespace WebAPI.Services.EquipmentService
{
    public class EquipmentService(DB_Context context) : IEquipmentService
    {
        #region Добавление оборудования
        public async Task<Equipment?> AddNewEquipment(NewEquipmentDTO newEquipment)
        {
            var equipmentIsExist = await context.Equipments
                .AnyAsync(e => e.InventoryNumber == newEquipment.InventoryNumber);

            if (equipmentIsExist)
                return null;

            var addedEquipment = await context.Equipments.AddAsync(new()
            {
                Name = newEquipment.Name,
                //Count = newEquipment.Count,
                InventoryNumber = newEquipment.InventoryNumber,
                Description = newEquipment.Description,
                TypeId = newEquipment.TypeId
            });
            await context.SaveChangesAsync();

            return addedEquipment.Entity;
        }

        public async Task<int> AddNewEquipments(IEnumerable<NewEquipmentDTO> newEquipments)
        {
            if (newEquipments.ToList().Count == 0)
                return 0;

            int i = 0;

            //проверка: существует ли такой тип оборудования
            foreach (var equip in newEquipments)
            {
                var typeIsExist = await context.EquipmentTypes.AnyAsync(et => et.Id == equip.TypeId);

                if (!typeIsExist)
                    return 0;

                i++;
            }

            await context.Equipments.AddRangeAsync(newEquipments.Select(eq => new Equipment()
            {
                Name = eq.Name,
                Description = eq.Description,
                InventoryNumber = eq.InventoryNumber,
                //Count = eq.Count,
                TypeId = eq.TypeId
            }));
            await context.SaveChangesAsync();

            return i;
        }

        public async Task<bool> AddNewEquipmentType(string typeName)
        {
            var equipTypeIsExist = await context.EquipmentTypes.AnyAsync(e => e.Name == typeName);

            if (equipTypeIsExist)
                return false;

            await context.EquipmentTypes.AddAsync(new EquipmentType()
            {
                Name = typeName
            });
            await context.SaveChangesAsync();

            return true;
        }
        #endregion

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public async Task<bool> DeleteEquipment(int id)
        {
            var equipmentOnDelete = await context.Equipments
                .FirstOrDefaultAsync(e => e.Id == id);

            if (equipmentOnDelete == null)
                return false;

            return true;
        }

        public async Task<int> DeleteEquipments(IEnumerable<EquipmentDTO> deletedEquipments)
        {
            if (deletedEquipments.ToList().Count == 0)
                return 0;

            int i = 0;

            foreach (var equip in deletedEquipments)
            {
                var typeIsExist = await context.EquipmentTypes.AnyAsync(et => et.Id == equip.EquipmentType.Id);

                if (!typeIsExist)
                    return 0;

                i++;
            }

            context.Equipments.RemoveRange(deletedEquipments.Select(eq => new Equipment()
            {
                Name = eq.Name,
                Description = eq.Description,
                InventoryNumber = eq.InventoryNumber,
                //Count = eq.Count,
                TypeId = eq.EquipmentType.Id
            }));
            await context.SaveChangesAsync();

            return i;
        }

        public async Task<EquipmentDTO?> GetEquipment(int id)
        {
            var foundedEquipment = await context.Equipments.FirstOrDefaultAsync(e => e.Id == id);

            if (foundedEquipment == null)
                return null;

            var equipmentType = await context.EquipmentTypes.FirstOrDefaultAsync(et => et.Id == foundedEquipment.TypeId);

            if (equipmentType == null)
                return null;

            EquipmentDTO equipment = new()
            {
                Id = foundedEquipment.Id,
                Description = foundedEquipment.Description,
                InventoryNumber = foundedEquipment.InventoryNumber,
                Name = foundedEquipment.Name,
                EquipmentType = equipmentType
            };

            return equipment;
        }

        public async Task<List<EquipmentDTO>?> GetEquipmentsById(IEnumerable<int> equipmentIds)
        {
            if (equipmentIds.ToList().Count == 0)
                return null;

            List<EquipmentDTO> equipments = [];

            foreach (var equipId in equipmentIds) 
            {
                var equipment = await context.Equipments
                    .Where(e => e.Id == equipId)
                    .Select(e => new EquipmentDTO()
                    {
                        Id = e.Id,
                        Description = e.Description,
                        EquipmentType = e.EquipmentType,
                        InventoryNumber = e.InventoryNumber,
                        Name = e.Name
                    })
                    .FirstOrDefaultAsync();

                if(equipment != null)
                    equipments.Add(equipment);
            }

            return equipments;
        }

        public async Task<IEnumerable<EquipmentDTO>> GetEquipments()
        {
            var equipments = await context.Equipments
                .Select(e => new EquipmentDTO() 
                { 
                    Id = e.Id,
                    Description = e.Description,
                    EquipmentType = context.EquipmentTypes.First(et => et.Id == e.TypeId),
                    InventoryNumber = e.InventoryNumber,
                    Name = e.Name
                })
                .ToListAsync();

            return equipments;
        }

        public async Task<bool> UpdateEquipment(int equipmentId, NewEquipmentDTO updateEquipment)
        {
            var foundedEquipment = await context.Equipments.FirstOrDefaultAsync(e => e.Id == equipmentId);

            if (foundedEquipment == null)
                return false;

            var inventoryNumberIsExist = await context.Equipments.AnyAsync(e => e.InventoryNumber == updateEquipment.InventoryNumber && e.Id != equipmentId);

            if (inventoryNumberIsExist)
                return false;

            foundedEquipment.Description = updateEquipment.Description;
            foundedEquipment.InventoryNumber = updateEquipment.InventoryNumber;
            foundedEquipment.Name = updateEquipment.Name;

            if(updateEquipment.TypeId > 0)
                foundedEquipment.TypeId = updateEquipment.TypeId;

            context.Equipments.Update(foundedEquipment);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<EquipmentType?> GetEquipmentType(int equipmentTypeId)
        {
            return await context.EquipmentTypes.FirstOrDefaultAsync(et => et.Id == equipmentTypeId);
        }

        public async Task<IEnumerable<EquipmentType>> GetEquipmentTypes()
        {
            return await context.EquipmentTypes.ToListAsync();
        }

        public async Task<int> GetEuipmentCountByType(string typeName)
        {
            var equipmentType = await context.EquipmentTypes
                .Where(et => et.Name.Equals(typeName, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefaultAsync();

            if (equipmentType == null)
                return 0;

            var equipments = await context.Equipments
                .Where(e => e.TypeId == equipmentType.Id)
                .ToListAsync();

            if (equipments == null)
                return 0;

            return equipments.Count;
        }
    }
}
