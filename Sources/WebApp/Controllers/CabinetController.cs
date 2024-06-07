using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLib.DTO;
using System.Security.Claims;
using System.Text.Json;
using WebApp.Models.PageModels;
using WebApp.Settings;

namespace WebApp.Controllers
{
    public class CabinetController : Controller
    {
        //Action для загрузки страницы кабинетов
        [HttpGet("cabinets")]
        [Authorize]
        public async Task<ActionResult> CabList()
        {
            List<CabinetDTO> cabs = (await AppSettings.Api.Client.GetFromJsonAsync<List<CabinetDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, "all"))) ?? [];

            return View(cabs);
        }

        [HttpGet("cabinets/my")]
        public async Task<ActionResult> ShowMyCabinets()
        {
            int userId = HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Sid)
                .Select(c => int.Parse(c.Value))
                .FirstOrDefault();

            var cabs = await AppSettings.Api.Client.GetFromJsonAsync<List<CabinetDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"cabinets-by-user={userId}"));

            return View("CabList", cabs);
        }

        [HttpGet("cabinets/user={userId}")]
        public async Task<ActionResult> ShowCabinetsByUsers([FromRoute] int userId) 
        {
            var cabs = await AppSettings.Api.Client.GetFromJsonAsync<List<CabinetDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"cabinets-by-user={userId}"));
            
            return View("CabList", cabs);
        }

        //Action для загрузки страницы с информацией об кабинете
        [HttpGet("cabinets/cabientId={id}")]
        [Authorize]
        public async Task<ActionResult> CabInfo([FromRoute] int id)
        {
            return View(await GetCabinetInfo(id));
        }

        //Action для поиска оборудования в кабинете
        [HttpPost("equip-search")]
        [Authorize]
        public async Task<ActionResult> SearchEquipment([FromHeader] string searchField, [FromHeader] int cabId, [FromHeader] string type)
        {
            var cabinetInfo = await GetCabinetInfo(cabId);

            cabinetInfo.SearchQuery = searchField;

            var searchData = type switch
            {
                "equipment" => (await AppSettings.Api.Client.GetFromJsonAsync<List<EquipmentDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"get-equip/cabid={cabId}")))?.Cast<dynamic>().ToList(),
                _ => []
            };

            cabinetInfo.Equipments = searchData ?? [];

            if (searchField != null)
                if (searchField.Length > 0)
                {
                    if (type == "equipment") {
                        cabinetInfo.Equipments = searchData?.Where(c =>
                            c.Name.Contains(searchField) ||
                            c.Description.Contains(searchField) ||
                            c.InventoryNumber.Contains(searchField)
                        ).Cast<dynamic>().ToList() ?? [];
                    }
                }

            return PartialView("CabInfo/_TableTemplatePartial", cabinetInfo);
        }

        [HttpGet("attach-equipments-to-cabinet")]
        [Authorize]
        public async Task<ActionResult> AttachEquipmentToCabinet([FromHeader] int cabId, [FromQuery] string r_equipmentsIds) 
        {
            var formated = r_equipmentsIds.Replace(@"\", "").Replace("\"", "");

            List<int>? equipments = JsonSerializer.Deserialize<List<int>>(formated);

            if(cabId != 0)
            {
                if (equipments != null)
                {
                    await AppSettings.Api.Client.PostAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"add-equip-to-cab/cabid={cabId}"), equipments);
                }
            }
            
            return Redirect($"../cabinets/cabientId={cabId}");
        }

        [HttpGet("show-equipments")]
        [Authorize]
        public async Task<ActionResult> ShowEquipments([FromHeader] int cabId)
        {
            List<EquipmentDTO>? equipment = await AppSettings.Api.Client.GetFromJsonAsync<List<EquipmentDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"get-equip/cabid={cabId}"));//$"api/Cabinet/get-equip/cabid={cabId}");

            return PartialView("CabInfo/_TableTemplatePartial", new CabInfoPage()
            {
                Equipments = equipment?.Cast<dynamic>().ToList() ?? [],
                SelectedList = "Equipment"
            });
        }

        /*[HttpGet("show-test")]
        [Authorize]
        public async Task<ActionResult> ShowTestTable() 
        {
            var testTable = new List<WebApp.Models.TableModels.TestData>() 
            {
                new()
                {
                    Id = 1,
                    Request = 2343,
                    From = new
                    {
                        Name = "TestName"
                    }
                }
            };

            return PartialView("CabInfo/_TableTemplatePartial", new CabInfoPage()
            {
                Equipments = testTable.Cast<dynamic>().ToList() ?? [],
                SelectedList = "TestData"
            });
        }*/

        [HttpGet("show-cabinet-requests")]
        [Authorize(Roles = "Master, Admin")]
        public async Task<ActionResult> ShowCabinetRequests([FromHeader] int cabId) 
        {
            Console.WriteLine(cabId);

            //http://localhost:5215/api/Request/requests/cabid=1
            List<RequestDTO>? requests = await AppSettings.Api.Client.GetFromJsonAsync<List<RequestDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Request, $"requests/cabid={cabId}")); //$"api/Request/requests/cabid={cabId}");


            return PartialView("CabInfo/_CabinetRequestsPartial", requests);
        }

        [HttpPost("send-data-cabinet")]
        [Authorize]
        public async Task<ActionResult> SendEditedCabinet([FromForm] CabinetDTO cabinet, [FromForm] int ResponsiblePerson, [FromForm] SendType sendType)
        {
            cabinet.ResponsiblePerson = new()
            {
                Id = ResponsiblePerson
            };

            var cabinetID = Request.Cookies["cabid"];

            AppSettings.Api.Client.DefaultRequestHeaders.Clear();
            AppSettings.Api.Client.DefaultRequestHeaders.Add("id", cabinetID);

            NewCabinetDTO cabinetData = new()
            {
                ResponsiblePersonId = cabinet.ResponsiblePerson.Id,
                Floor = cabinet.Floor,
                Height = cabinet.Height,
                Length = cabinet.Length,
                Num = cabinet.Num,
                PlanNum = cabinet.PlanNum,
                Width = cabinet.Width
            };

            if (sendType == SendType.Edit)
                await AppSettings.Api.Client.PutAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, "update"), cabinetData);

            if (sendType == SendType.New)
                await AppSettings.Api.Client.PostAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, "new"), cabinetData);

            return Redirect($"/cabinets/cabientId={cabinetID}");
        }

        [HttpPost("send-data-equipment")]
        [Authorize]
        public async Task<ActionResult> SendEditedEquipment([FromForm] EquipmentDTO equipment, [FromForm] int EquipmentType, [FromForm] SendType sendType) 
        {
            equipment.EquipmentType = new()
            {
                Id = EquipmentType
            };

            var cabinetID = Request.Cookies["cabid"];

            AppSettings.Api.Client.DefaultRequestHeaders.Clear();
            AppSettings.Api.Client.DefaultRequestHeaders.Add("id", equipment.Id.ToString());

            NewEquipmentDTO equipmentData = new()
            {
                Description = equipment.Description,
                InventoryNumber = equipment.InventoryNumber,
                Name = equipment.Name,
                TypeId = EquipmentType
            };

            if(sendType == SendType.Edit)
                await AppSettings.Api.Client.PutAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.Equipment, "update"), equipmentData);

            if (sendType == SendType.New)
                await AppSettings.Api.Client.PostAsJsonAsync(AppSettings.Api.ApiRequestUrl(ApiRequestType.Equipment, "new"), equipmentData);

            return Redirect($"/cabinets/cabientId={cabinetID}");
        }


        private static async Task<CabInfoPage> GetCabinetInfo(int cabId) 
        {
            var cabinet = await AppSettings.Api.Client.GetFromJsonAsync<CabinetDTO>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, $"get/id={cabId}"));//$"api/Cabinet/get/id={cabId}");
            //List<EquipmentDTO>? equipment = await apiHttpClient.GetFromJsonAsync<List<EquipmentDTO>>($"api/Cabinet/get-equip/id={cabId}");

            return new CabInfoPage()
            {
                Cabinet = cabinet ?? new(),
                //Equipments = equipment.Cast<dynamic>().ToList()
            };
        }
    }
}
