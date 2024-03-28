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

        public async Task<Cabinet?> AddCabinet(NewCabinetDTO newCabinet)
        {
            var cabinets = await _context.Cabinets.ToListAsync();

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

            await _context.AddAsync(addedCabinet);
            await _context.SaveChangesAsync();

            return addedCabinet;
        }

        public async Task<bool> DeleteCabinet(int id)
        {
            var cabinet = await _context.Cabinets
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cabinet == null)
                return false;

            _context.Cabinets.Remove(cabinet);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Cabinet?> GetCabinet(int id)
        {
            var cabinet = await _context.Cabinets
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cabinet == null)
                return null;

            return cabinet;
        }

        public async Task<IEnumerable<CabinetDTO>> GetCabinets()
        {
            var cabs = await _context.Cabinets.ToListAsync();

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

                var resposiblePerson = await _context.Users
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
