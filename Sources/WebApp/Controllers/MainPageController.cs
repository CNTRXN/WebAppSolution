using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Models.DTO;
using WebApp.Settings;

namespace WebApp.Controllers
{
    public class MainPageController : Controller
    {
        [HttpGet]
        public IActionResult Main() 
        {
            /*if (HttpContext.Request.Cookies.ContainsKey("state-id")) 
            {
                var stateId = HttpContext.Request.Cookies["state-id"];

                Console.WriteLine(stateId);
            }*/

            if (!(HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated))
                //return Redirect("../auth");
                return Redirect("../test-page");



            //if (AppStatics.User is UserDTO user) 
            //{
            //    return View(user);
            //}
            int? userId = null;
            if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value is string userIdAsString)
                userId = int.Parse(userIdAsString);

            return Redirect($"../profile={userId}");
        }
    }
}
