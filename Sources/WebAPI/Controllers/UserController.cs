using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using ModelLib.Model;
using ModelLib.DTO;
using EncryptLib;
using System.Text;
using WebAPI.Services.UserService;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Services.Notification;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService, IHubContext<NotificationService> hubContext) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = (await userService.GetUsers()).ToList();

            if (users.Count == 0)
                return BadRequest("В БД нет записей пользователей");

            return Ok(users);
        }

        [HttpGet("get/id={id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var foundedUser = await userService.GetUser(id);

            if (foundedUser == null)
                return BadRequest($"Пользователь с id '{id}' не найден");

            return Ok(foundedUser);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetUserByLoginAndPassword([FromHeader] string login, [FromHeader] string password)
        {
            login = await Encrypting.Decrypt(Convert.FromBase64String(login));
            password = await Encrypting.Decrypt(Convert.FromBase64String(password));

            var foundedUser = await userService.GetUser(login, password);

            if (foundedUser == null)
                return BadRequest("Пользователь не найден");

            return Ok(foundedUser);
        }

        [HttpGet("get-detail={id}")]
        public async Task<IActionResult> GetDetailUserInfo([FromRoute] int id) 
        {
            var foundedUser = await userService.GetDetailUserInfo(id);

            if (foundedUser == null)
                return BadRequest("Пользователь не найден");

            return Ok(foundedUser);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> AddUser([FromBody] NewUserDTO newUser) 
        {
            var addedUser = await userService.AddUser(newUser);

            if (addedUser == null)
                return BadRequest("Не удалось добавить пользователя!");

            return Ok(addedUser);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromHeader] int id)
        {
            var deletedUser = await userService.DeleteUser(id);

            if (!deletedUser)
                return BadRequest("Не удалось удалить пользователя");

            return Ok($"Пользователь с id '{id}' удалён");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromHeader] int id, [FromBody] NewUserDTO updateUser) 
        {
            var updateUserResult = await userService.UpdateUser(id, updateUser);

            var senderId = HttpContext.Request.Cookies["sender-id"];
            var connectionId = NotificationService.ConnectedUsers
                .Where(cu => cu.UserId == senderId)
                .Select(cu => cu.ConnectionId)
                .FirstOrDefault();

            if (!updateUserResult)
                return BadRequest("Ошибка обновления записи");

            if (connectionId != null)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("UpdatePage");
            }

            return Ok("Запись успешно обновлена");
        }
    }
}