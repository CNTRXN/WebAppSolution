using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.PermissionService
{
    public class PermissionService(DB_Context context) : IPermissionService
    {
        public async Task<Permission?> AddPermission(PermissionDTO newPermission)
        {
            var premissionIsExist = await context.Permissions
                .AnyAsync(p => p.Name.Equals(newPermission.Name, StringComparison.CurrentCultureIgnoreCase));

            if (premissionIsExist)
                return null;

            var addedPermission = await context.Permissions.AddAsync(new Permission()
            {
                Name = newPermission.Name
            });

            await context.SaveChangesAsync();

            return addedPermission.Entity;
        }

        public async Task<bool> DeletePermission(int id)
        {
            var primission = await context.Permissions
               .Where(p => p.Id == id)
               .FirstOrDefaultAsync();

            if (primission == null)
                return false;

            context.Remove(primission);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<Permission?> GetPermission(string name)
        {
            var premission = await context.Permissions.FirstAsync(p => p.Name == name);

            if (premission == null)
                return null;

            return premission;
        }

        public async Task<Permission?> GetPermission(int id)
        {
            var premission = await context.Permissions.FirstAsync(p => p.Id == id);

            if (premission == null)
                return null;

            return premission;
        }

        public async Task<IEnumerable<Permission>> GetPermissions()
        {
            return await context.Permissions.ToListAsync();
        }
    }
}
