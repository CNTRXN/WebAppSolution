using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class InfoController : Controller
    {
        public ActionResult About()
        {
            return View();
        }

        [HttpGet("test-page")]
        //[Authorize(Roles = "Master, Admin")]
        public ActionResult Test() 
        {
            return View();
        }
    }
}
