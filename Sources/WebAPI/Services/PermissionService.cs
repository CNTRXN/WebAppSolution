using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services
{
    public class PermissionService(DB_Context context) : IPermissionService
    {
        private readonly DB_Context _context = context;
        public async Task<Permission?> AddPermission(PermissionDTO newPermission)
        {
            var premissionIsExist = await _context.Permissions
                .AnyAsync(p => p.Name.Equals(newPermission.Name, StringComparison.CurrentCultureIgnoreCase));

            if (premissionIsExist)
                return null;

            var addedPermission = await _context.Permissions.AddAsync(new Permission()
            {
                Name = newPermission.Name
            });

            await _context.SaveChangesAsync();

            return addedPermission.Entity;
        }

        public async Task<bool> DeletePermission(int id)
        {
            var primission = await _context.Permissions
               .Where(p => p.Id == id)
               .FirstOrDefaultAsync();

            if (primission == null)
                return false;

            _context.Remove(primission);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Permission?> GetPermission(string name)
        {
            var premission = await _context.Permissions.FirstAsync(p => p.Name == name);

            if (premission == null)
                return null;

            return premission;
        }

        public async Task<Permission?> GetPermission(int id)
        {
            var premission = await _context.Permissions.FirstAsync(p => p.Id == id);

            if (premission == null)
                return null;

            return premission;
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }
    }
}
