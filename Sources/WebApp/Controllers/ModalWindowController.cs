using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
//using WebApp.Models.TableModels;
using ModelLib.DTO;
using WebApp.Settings;
using WebApp.Models.PageModels;
using ModelLib.Model;

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
            CabinetDTO? cabinet = cabId != 0 ? await AppSettings.Api.Client.GetFromJsonAsync<CabinetDTO>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"get/id={cabId}")) : null;//$"api/Cabinet/get/id={cabId}") : null;

            List<int>? equipmentIds = JsonSerializer.Deserialize<List<int>>(r_equipmentIds);

            List<EquipmentDTO>? equipments = null;
            if (cabId != 0)
            {
                AppSettings.Api.Client.DefaultRequestHeaders.Clear();

                //var json = await JsonSerializer.SerializeAsync();

                //AppStatics.ApiClient.DefaultRequestHeaders.Add("", "");
                //equipments = equipmentId != 0 ? await AppStatics.ApiClient.GetFromJsonAsync<List<EquipmentDTO>>($"api/Equipment/get/id={equipmentId}") : null;
                if(equipmentIds != null)
                {
                    /*var queryString = string.Join("&", equipmentIds
                        .Select(x => $"{Uri.EscapeDataString(x)}={Uri.EscapeDataString(x.Value)}"));*/
                    var queryString = string.Join("&", equipmentIds
                        .Select(x => $"ids={x}"));

                    equipments = await AppSettings.Api.Client.GetFromJsonAsync<List<EquipmentDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Equipment, $"get/byids?{queryString}"));//$"api/Equipment/get/byids?{queryString}");
                    /*foreach (var equipId in equipmentIds)
                    {
                    

                    }*/
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
                "cabinets" => cabId != 0 ? await AppSettings.Api.Client.GetFromJsonAsync<List<CabinetDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"get/cabid={cabId}"))
                    : await AppSettings.Api.Client.GetFromJsonAsync<List<CabinetDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, "all")),
                "equipments" => cabId != 0 ? await AppSettings.Api.Client.GetFromJsonAsync<List<EquipmentDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"get-equip/cabid={cabId}")) 
                    : await AppSettings.Api.Client.GetFromJsonAsync<List<EquipmentDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Equipment, $"all")),
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

            await AppSettings.Api.Client.PostAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.Request, "repair"), multipartContent);

            return Redirect("/");
        }
        #endregion

        #region Форма редактирования информации о кабинете
        [HttpGet("show-cabinet-edit-form")]
        [Authorize]
        public async Task<ActionResult> ShowCabinetEditForm([FromHeader] int cabId) 
        {
            //http://localhost:5215/api/Cabinet/get/id=1

            CabinetDTO? cabinet = await AppSettings.Api.Client.GetFromJsonAsync<CabinetDTO>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"get/id={cabId}")); //$"api/Cabinet/get/id={cabId}");

            return PartialView("CabInfo/ModalWindows/EditInfoForm/_CabinetInfoEditFormPartial", cabinet);
        }

        [HttpGet("show-cabinet-edit-form-resp-person")]
        [Authorize]
        public async Task<ActionResult> ShowResponsiblePersonsList() 
        {
            return PartialView("CabInfo/ModalWindows/EditInfoForm/_CabinetInfoEditFormResposiblePersonPartial");
        }
        #endregion

        //Форма добавления/изменения информации
        #region Форма добавления/изменения информации
        [HttpGet("show-editor-form")]
        [Authorize]
        public async Task<ActionResult> ShowAddEditForm([FromHeader] int infoTypeCode, [FromHeader] int? itemId, [FromHeader] int sendType) 
        {
            Console.WriteLine($"code: {infoTypeCode}\nid: {itemId}");

            DataEditor data = new() 
            {
                TypeName = infoTypeCode switch
                {
                    1 => "CabinetDTO",
                    2 => "EquipmentDTO",
                    3 => "UpdateUserDTO",
                    _ => string.Empty
                },
                DataValue = itemId != null ? infoTypeCode switch 
                {
                    1 => await AppSettings.Api.Client.GetFromJsonAsync<CabinetDTO>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"get/id={itemId}")),
                    2 => await AppSettings.Api.Client.GetFromJsonAsync<EquipmentDTO>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Equipment, $"get/id={itemId}")),
                    3 => await AppSettings.Api.Client.GetFromJsonAsync<UpdateUserDTO>(AppSettings.Api.ApiRequestUrl(ApiRequestType.User, $"get-detail={itemId}")),
                    _ => null
                } : null,
                SendType = sendType switch 
                {
                    1 => SendType.New,
                    2 => SendType.Edit,
                    _ => SendType.New
                }
            };

            return PartialView("_EditInfoForm", data);
        }
        #endregion
    }
}
