using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("accessdenied")]
        public ActionResult AccessDenied()
        {
            ErrorMessage error = new() 
            {
                StatusCode = 403,
                Message = "Доступ запрещён"
            };

            return View("_ErrorPage", error);
        }

        [HttpGet("error{statusCode}")]
        public ActionResult ErrorShow([FromRoute] int statusCode) 
        {
            ErrorMessage error = new()
            {
                StatusCode = statusCode,
                Message = statusCode switch 
                {
                    404 => "Ресурс не найден",
                    _ => string.Empty
                }
            };

            return View("_ErrorPage", error);
        }
    }
}
