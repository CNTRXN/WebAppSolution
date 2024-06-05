using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApp.Controllers
{
    public class MainPageController : Controller
    {
        [HttpGet]
        public IActionResult Main() 
        {
            if (!(HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated))
                return Redirect("../auth");
                
                //return Redirect("../test-page");

            int? userId = null;
            if (HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value is string userIdAsString)
                userId = int.Parse(userIdAsString);

            //return Redirect($"../profile={userId}");
            //return Redirect($"../cabinets");
            return Redirect("../cabinets/my");
        }
    }
}