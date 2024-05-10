using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
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
        [Authorize(Roles = "Master, Admin")]
        public ActionResult Test() 
        {
            return View();
        }

        [HttpGet("show-request-from")]
        [Authorize]
        public async Task<ActionResult> ShowRequestForm([FromHeader] int cabId) 
        {
            CabinetDTO? cabinet = cabId != 0 ? await AppStatics.ApiClient.GetFromJsonAsync<CabinetDTO>($"api/Cabinet/get/id={cabId}") : null;
            List<EquipmentDTO>? equipments = null;

            return PartialView("_RequestFormPartial", (cabinet, equipments));
        }

        [HttpGet("show-select-object")]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult> SendReqeust(
            [FromForm] string cabinet, 
            [FromForm] List<string> equipment,
            [FromForm] List<IFormFile> images) 
        {
            int? senderId = null;
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                if (HttpContext.User.FindFirstValue(ClaimTypes.Sid) is string userId)
                    senderId = int.Parse(userId);

            MultipartFormDataContent multipartContent = new()
            {
                { new StringContent(cabinet), "cabinetId" },
                { new StringContent(senderId.ToString()), "userId" },
                { new StringContent("Test Title"), "Title" },
                { new StringContent("Test Description"), "Description" },
            };

            equipment.Remove(equipment[^1]);
            foreach (var equip in equipment)
            {
                multipartContent.Add(new StringContent(equip.ToString()), "equipmentsIds");
            }

            foreach (var image in images)
            {
                multipartContent.Add(new StreamContent(image.OpenReadStream())
                {
                    Headers =
                    {
                        ContentLength = image.Length,
                        ContentType = new MediaTypeHeaderValue(image.ContentType)
                    }
                }, "images", image.FileName);
            }

            var response = await AppStatics.ApiClient.PostAsync("api/Request/repair", multipartContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("request send");
            }
            else 
            {
                Console.WriteLine("error");
            }

            return Redirect("/test-page");
        }
    }
}
