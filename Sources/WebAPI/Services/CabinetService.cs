using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.Other;

namespace WebAPI.Services
{
    public class CabinetService(DB_Context context) : ICabinetService
    {
        private readonly DB_Context _context = context;

        public async Task<Cabinet?> AddCabinet(CabinetDTO cabinet)
        {
            var cabinets = await _context.Cabinets.ToListAsync();

            if (EqualEntity.CabinetIsExist(cabinets, cabinet))
                return null;

            var newCabinet = new Cabinet()
            {
                Num = cabinet.Num,
                PlanNum = cabinet.PlanNum,
                Floor = cabinet.Floor,
                Group = cabinet.Group,
                Height = cabinet.Height,
                Length = cabinet.Length,
                Width = cabinet.Width,
                ResponsiblePersonId = cabinet.ResponsiblePersonId
            };

            await _context.AddAsync(newCabinet);
            await _context.SaveChangesAsync();

            return newCabinet;
        }

        public async Task<bool> DeleteCabinet(int id)
        {
            var cabinet = await _context.Cabinets.FirstOrDefaultAsync(c => c.Id == id);

            if (cabinet == null)
                return false;

            _context.Cabinets.Remove(cabinet);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Cabinet?> GetCabinet(int id)
        {
            var cabinet = await _context.Cabinets.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (cabinet == null)
                return null;

            return cabinet;
        }

        public async Task<IEnumerable<Cabinet>> GetCabinets()
        {
            return await _context.Cabinets.ToListAsync();
        }
    }
}
