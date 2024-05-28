using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLib.DTO;
using ModelLib.Model;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("users-list")]
        public async Task<ActionResult> UsersList() 
        {
            return View();
        }

        [HttpPost("send-edit-data-user")]
        public async Task<ActionResult> SendEditedUser([FromForm] UserDTO userData, [FromForm] int Permission) 
        {
            userData.Permission = new() 
            {
                Id = Permission
            };

            Console.WriteLine($"" +
                $"{userData.Id}\n" +
                $"{userData.Name}\n" +
                $"{userData.Surname}\n" +
                $"{userData.Patronymic}\n" +
                $"{userData.Birthday}\n" +
                $"{userData.Permission.Id}");

            return Ok();
        }
    }
}
