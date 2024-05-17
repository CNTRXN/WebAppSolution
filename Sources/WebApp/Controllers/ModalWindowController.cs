using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using WebApp.Models.DTO;
using WebApp.Settings;

namespace WebApp.Controllers
{
    public class ModalWindowController : Controller
    {
        [HttpGet("open-image-form")]
        [Authorize]
        public async Task<ActionResult> ShowCabinetsPhoto([FromHeader] int cabId)
        {
            //List<string>? imageHref = await AppStatics.ApiClient.GetFromJsonAsync<List<string>>($"api/Cabinet/image/getImagesByCab={cabId}");

            return PartialView("CabInfo/ModalWindows/_CabinetImageFormPartial", cabId);
        }

        //!!!!!
        #region Форма отправки заявки
        [HttpGet("show-request-form")]
        [Authorize]
        public async Task<ActionResult> ShowRequestForm([FromHeader] int cabId, [FromQuery] string r_equipmentIds)
        {
            CabinetDTO? cabinet = cabId != 0 ? await AppStatics.ApiClient.GetFromJsonAsync<CabinetDTO>($"api/Cabinet/get/id={cabId}") : null;

            List<int>? equipmentIds = JsonSerializer.Deserialize<List<int>>(r_equipmentIds);

            List<EquipmentDTO>? equipments = null;
            if (cabId != 0)
            {
                AppStatics.ApiClient.DefaultRequestHeaders.Clear();

                //var json = await JsonSerializer.SerializeAsync();

                //AppStatics.ApiClient.DefaultRequestHeaders.Add("", "");
                //equipments = equipmentId != 0 ? await AppStatics.ApiClient.GetFromJsonAsync<List<EquipmentDTO>>($"api/Equipment/get/id={equipmentId}") : null;
                if(equipmentIds != null)
                {
                    foreach (var equipId in equipmentIds)
                    {
                        //Console.WriteLine(equipId);

                    }
                }
            }

            return PartialView("RequestForm/_RequestFormPartial", (cabinet, equipments));
        }

        [HttpGet("show-select-object")]
        [Authorize]
        public async Task<ActionResult> ShowSelectObject([FromHeader] int cabId, [FromHeader] string getType)
        {
            dynamic? getData = getType switch
            {
                "cabinets" => cabId != 0 ? await AppStatics.ApiClient.GetFromJsonAsync<List<CabinetDTO>>($"api/Cabinet/get/cabid={cabId}")
                    : await AppStatics.ApiClient.GetFromJsonAsync<List<CabinetDTO>>($"api/Cabinet/all"),
                "equipments" => await AppStatics.ApiClient.GetFromJsonAsync<List<EquipmentDTO>>($"api/Cabinet/get-equip/cabid={cabId}"),
                _ => null
            };

            return PartialView("RequestForm/_SelectObjectFormPartial", getData);
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

            /*var response = await AppStatics.ApiClient.PostAsync("api/Request/repair", multipartContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("request send");
            }
            else
            {
                Console.WriteLine("error");
            }*/

            return Redirect("/");
        }
        #endregion

        #region Форма редактирования информации о кабинете
        [HttpGet("/show-cabinet-edit-form")]
        [Authorize]
        public async Task<ActionResult> ShowCabinetEditForm([FromHeader] int cabId) 
        {
            //http://localhost:5215/api/Cabinet/get/id=1
            CabinetDTO? cabinet = await AppStatics.ApiClient.GetFromJsonAsync<CabinetDTO>($"api/Cabinet/get/id={cabId}");

            return PartialView("CabInfo/ModalWindows/EditInfoForm/_CabinetInfoEditFormPartial", cabinet);
        }

        [HttpGet("/show-cabinet-edit-form-resp-person")]
        [Authorize]
        public async Task<ActionResult> ShowResponsiblePersonsList() 
        {
            return PartialView("CabInfo/ModalWindows/EditInfoForm/_CabinetInfoEditFormResposiblePersonPartial");
        }
        #endregion
    }
}
