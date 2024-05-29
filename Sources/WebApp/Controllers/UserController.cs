using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLib.DTO;
using ModelLib.Model;
using WebApp.Models.PageModels;
using WebApp.Settings;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("users-list")]
        public async Task<ActionResult> UsersList() 
        {
            return View();
        }

        [HttpPost("send-data-user")]
        public async Task<ActionResult> SendEditedUser([FromForm] UpdateUserDTO userData, [FromForm] int Id, [FromForm] int Permission, [FromForm] SendType sendType) 
        {
            AppSettings.Api.Client.DefaultRequestHeaders.Clear();
            AppSettings.Api.Client.DefaultRequestHeaders.Add("id", Id.ToString());

            NewUserDTO updateUser = new()
            {
                PermissionId = Permission,
                Birthday = userData.Birthday,
                Login = userData.Login,
                Name = userData.Name,
                Password = userData.Password,
                Patronymic = userData.Patronymic,
                Surname = userData.Surname
            };

            await AppSettings.Api.Client.PutAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.User, "update"), updateUser);

            /*Console.WriteLine(sendType.ToString());

            Console.WriteLine($"" +
                $"{userData.Id}\n" +
                $"{userData.Name}\n" +
                $"{userData.Surname}\n" +
                $"{userData.Patronymic}\n" +
                $"{userData.Birthday}\n" +
                $"{userData.Permission.Id}");*/

            return Ok();
        }
    }
}
