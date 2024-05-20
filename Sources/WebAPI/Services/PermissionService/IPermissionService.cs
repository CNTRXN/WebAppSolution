using ModelLib.Model;
using ModelLib.DTO;

namespace WebAPI.Services.PermissionService
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetPermissions();
        Task<Permission?> GetPermission(string name);
        Task<Permission?> GetPermission(int id);
        Task<Permission?> AddPermission(string newPermission);
        Task<bool> DeletePermission(int id);
    }
}
