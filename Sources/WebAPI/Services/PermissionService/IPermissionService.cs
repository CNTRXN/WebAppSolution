using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.PermissionService
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission?> GetPermission(string name);
        Task<Permission?> GetPermission(int id);
        Task<Permission?> AddPermission(PermissionDTO newPermission);
        Task<bool> DeletePermission(int id);
    }
}
