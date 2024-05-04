using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApp.Models;
using WebApp.Models.DTO;
using WebApp.Settings;

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
        public async Task<ActionResult> ShowRequestForm([FromHeader] int cabId) 
        {
            CabinetDTO? cabinet = cabId != 0 ? await AppStatics.ApiClient.GetFromJsonAsync<CabinetDTO>($"api/Cabinet/get/id={cabId}") : null;
            List<EquipmentDTO>? equipments = null;

            return PartialView("_RequestFormPartial", (cabinet, equipments));
        }

        [HttpGet("show-select-object")]
        public async Task<ActionResult> ShowSelectObject([FromHeader] int cabId, [FromHeader] string getType) 
        {
            dynamic? getData = getType switch
            {
                "cabinets" => cabId != 0 ? await AppStatics.ApiClient.GetFromJsonAsync<CabinetDTO>($"api/Cabinet/get/id={cabId}") 
                    : await AppStatics.ApiClient.GetFromJsonAsync<List<CabinetDTO>>($"api/Cabinet/all"),
                "equipments" => await AppStatics.ApiClient.GetFromJsonAsync<List<EquipmentDTO>>($"api/Cabinet/get-equip/id={cabId}"),
                _ => null
            };

            return PartialView("_SelectObjectFormPartial", getData);
        }

        [HttpPost("send-request")]
        public ActionResult SendReqeust(
            [FromForm] string cabinet, 
            [FromForm] List<string> equipment,
            [FromForm] List<IFormFile> images) 
        {
            var files = images;

            Console.WriteLine(files);
            //var files = HttpContext.Request.Files
            /*var formData = HttpContext.Request.Form;

            int cabinetId = int.Parse(formData["cabinet"]);
            //List<int> equipmentsId = formData["equipment"].Select(int.Parse).ToList();
            string description = formData["description"];

            var images = formData.Files;*/

            /*Console.WriteLine(cabinet);
            foreach (var equip in equipment) 
            {
                Console.WriteLine(equip);
            }*/
            //Console.WriteLine(images);

            return Redirect("/test-page");
        }
    }
}
