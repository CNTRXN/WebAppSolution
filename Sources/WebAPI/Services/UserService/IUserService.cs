using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUser(int id);
        Task<UserDTO?> GetUser(string login, string password);
        Task<User?> AddUser(NewUserDTO newUser);
        Task<bool> DeleteUser(int id);
    }
}
