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
            List<UserDTO> users = (await AppSettings.Api.Client.GetFromJsonAsync<List<UserDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.User, "all"))) ?? [];

            return View(users);
        }

        [HttpPost("send-data-user")]
        public async Task<ActionResult> SendEditedUser([FromForm] UpdateUserDTO userData, [FromForm] int Id, [FromForm] int Permission, [FromForm] SendType sendType) 
        {
            AppSettings.Api.SetHeaders(new Dictionary<string, string>() 
            {
                { "Id", Id.ToString() }
            });

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

            if(sendType == SendType.Edit)
                await AppSettings.Api.Client.PutAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.User, "update"), updateUser);

            if(sendType == SendType.New)
                await AppSettings.Api.Client.PostAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.User, "registration"), updateUser);

            return Redirect("../users-list");
        }
    }
}
