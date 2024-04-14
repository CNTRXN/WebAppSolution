using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services
{
    public class EquipmentService(DB_Context context) : IEquipmentService
    {
        private readonly DB_Context _context = context;
        public Task<Equipment?> AddNewEquipment(EquipmentDTO newEquipments)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> AddNewEquipments(IEnumerable<EquipmentDTO> newEquipments)
        {
            if (newEquipments.ToList().Count == 0)
                return null;

            int i = 0;

            foreach (var equip in newEquipments)
            {
                var typeIsExist = await _context.EquipmentTypes.AnyAsync(et => et.Id == equip.TypeId);

                if (!typeIsExist)
                    return null;

                i++;
            }

            await _context.Equipments.AddRangeAsync(newEquipments.Select(eq => new Equipment()
            {
                Name = eq.Name,
                Description = eq.Description,
                Count = eq.Count,
                TypeId = eq.TypeId
            }));
            await _context.SaveChangesAsync();

            return i;
        }

        public async Task<bool> AddNewEquipmentType(string typeName)
        {
            var equipTypeIsExist = await _context.EquipmentTypes.AnyAsync(e => e.Name == typeName);

            if (equipTypeIsExist)
                return false;

            await _context.EquipmentTypes.AddAsync(new EquipmentType() 
            {
                Name = typeName
            });
            await _context.SaveChangesAsync();

            return true;
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public Task<bool> DeleteEquipment(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> DeleteEquipments(IEnumerable<EquipmentDTO> deletedEquipments)
        {
            if (deletedEquipments.ToList().Count == 0)
                return null;

            int i = 0;

            foreach (var equip in deletedEquipments)
            {
                var typeIsExist = await _context.EquipmentTypes.AnyAsync(et => et.Id == equip.TypeId);

                if (!typeIsExist)
                    return null;

                i++;
            }

            _context.Equipments.RemoveRange(deletedEquipments.Select(eq => new Equipment()
            {
                Name = eq.Name,
                Description = eq.Description,
                Count = eq.Count,
                TypeId = eq.TypeId
            }));
            await _context.SaveChangesAsync();

            return i;
        }

        public async Task<Equipment?> GetEquipment(int id)
        {
            return await _context.Equipments.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Equipment>> GetEquipments()
        {
            return await _context.Equipments.ToListAsync();
        }
    }
}
