using ModelLib.Model;
using ModelLib.DTO;

namespace WebAPI.Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO?> GetUser(int id);
        Task<UserDTO?> GetUser(string login, string password);
        Task<UpdateUserDTO?> GetDetailUserInfo(int id);
        Task<User?> AddUser(NewUserDTO newUser);
        Task<bool> DeleteUser(int id);
        Task<bool> UpdateUser(int id, NewUserDTO updateUser);
    }
}
