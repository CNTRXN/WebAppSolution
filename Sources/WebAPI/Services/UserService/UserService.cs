using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.UserService
{
    public class UserService(DB_Context context) : IUserService
    {
        public async Task<User?> AddUser(NewUserDTO newUser)
        {
            var userIsExist = context.Users.Any(p => p.Login == newUser.Login);
            var postIsExist = context.Permissions.Any(p => p.Id == newUser.PostId);

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

            var addedUser = await context.Users.AddAsync(newUserData);
            await context.SaveChangesAsync();

            return addedUser.Entity;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var foundedUser = await context.Users
                .Where(u => u.Id.ToString() == id.ToString())
                .FirstOrDefaultAsync();

            if (foundedUser == null)
                return false;

            context.Users.Remove(foundedUser);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<User?> GetUser(int id)
        {
            var foundedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (foundedUser == null)
                return null;

            return foundedUser;
        }

        public async Task<UserDTO?> GetUser(string login, string password)
        {
            var foundedUser = await context.Users
                 .Where(u => u.Login == login && u.Password == password)
                 .FirstOrDefaultAsync();

            if (foundedUser == null)
                return null;

            var userPost = await context.Permissions
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
            return await context.Users.ToListAsync();
        }
    }
}
