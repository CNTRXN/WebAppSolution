using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services
{
    public class UserService(DB_Context context) : IUserService
    {
        private readonly DB_Context _context = context;

        public async Task<User?> AddUser(NewUserDTO newUser)
        {
            var userIsExist = _context.Users.Any(p => p.Login == newUser.Login);
            var postIsExist = _context.Permissions.Any(p => p.Id == newUser.PostId);

            if (userIsExist && !postIsExist)
                return null;

            var newUserData = new User()
            {
                Password = newUser.Password,
                Name = newUser.Name,
                Birthday = newUser.Birthday,
                Surname = newUser.Surname,
                Patronymic = newUser.Patronymic,
                Login = newUser.Login,
                PermissionId = newUser.PostId,
            };

            var addedUser = await _context.Users.AddAsync(newUserData);
            await _context.SaveChangesAsync();

            return addedUser.Entity;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var foundedUser = await _context.Users
                .Where(u => u.Id.ToString() == id.ToString())
                .FirstOrDefaultAsync();

            if (foundedUser == null)
                return false;

            _context.Users.Remove(foundedUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUser(int id)
        {
            var foundedUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (foundedUser == null)
                return null;

            return foundedUser;
        }

        public async Task<UserDTO?> GetUser(string login, string password)
        {
            var foundedUser = await _context.Users
                 .Where(u => u.Login == login && u.Password == password)
                 .FirstOrDefaultAsync();

            if (foundedUser == null)
                return null;

            var userPost = await _context.Permissions
                .Where(p => p.Id == foundedUser.PermissionId)
                .FirstAsync();

            var user = new UserDTO()
            {
                Id = foundedUser.Id,
                Name = foundedUser.Name,
                Surname = foundedUser.Surname,
                Patronymic = foundedUser.Patronymic,
                Birthday = foundedUser.Birthday,
                PermissionName = userPost.Name
            };

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
