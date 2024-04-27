using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApp.Models.DTO;

namespace WebApp.Controllers
{
    public class InfoController : Controller
    {
        public ActionResult About()
        {
            return View();
        }

        [HttpGet("test-page")]
        public ActionResult Test() 
        {
            return View();
        }

        [HttpGet("show-request-from")]
        public ActionResult ShowRequestForm() 
        {
            return PartialView("_RequestFormPartial", null);
        }

        [HttpPost("send-request")]
        public ActionResult SendReqeust() 
        {
            return Redirect("/");
        }
    }
}
