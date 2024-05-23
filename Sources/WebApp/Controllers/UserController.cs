using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("users-list")]
        public async Task<ActionResult> UsersList() 
        {
            return View();
        }
    }
}
