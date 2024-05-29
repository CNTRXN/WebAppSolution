using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using ModelLib.Model;
using ModelLib.DTO;

namespace WebAPI.Services.UserService
{
    public class UserService(DB_Context context) : IUserService
    {
        public async Task<User?> AddUser(NewUserDTO newUser)
        {
            var userIsExist = context.Users.Any(p => p.Login == newUser.Login);
            var postIsExist = context.Permissions.Any(p => p.Id == newUser.PermissionId);

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
                PermissionId = newUser.PermissionId,
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

        public async Task<bool> UpdateUser(int id, NewUserDTO updateUser)
        {
            var foundedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (foundedUser == null)
                return false;

            foundedUser.Password = updateUser.Password;
            foundedUser.Birthday = updateUser.Birthday;
            foundedUser.Name = updateUser.Name;
            foundedUser.Surname = updateUser.Surname;
            foundedUser.Patronymic = updateUser.Patronymic;

            if (updateUser.PermissionId > 0) 
            {
                var permissionIsExist = await context.Permissions.AnyAsync(p => p.Id == updateUser.PermissionId);

                if (!permissionIsExist)
                    return false;

                foundedUser.PermissionId = updateUser.PermissionId;
            }

            context.Users.Update(foundedUser);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<UserDTO?> GetUser(int id)
        {
            var foundedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

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
                Permission = userPost
            };

            return user;
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
                Permission = userPost
            };

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<UpdateUserDTO?> GetDetailUserInfo(int id) 
        {
            var foundedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (foundedUser == null)
                return null;

            UpdateUserDTO detailInfo = new() 
            {
                Birthday = foundedUser.Birthday,
                Login = foundedUser.Login,
                Password = foundedUser.Password,
                Patronymic = foundedUser.Patronymic,
                Name = foundedUser.Name,
                Surname = foundedUser.Surname
            };

            var permission = await context.Permissions.FirstOrDefaultAsync(p => p.Id == foundedUser.PermissionId);

            detailInfo.Permission = permission ?? new Permission() 
            {
                Id = 1
            };

            return detailInfo;
        }
    }
}
