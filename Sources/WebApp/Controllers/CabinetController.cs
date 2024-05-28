using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLib.DTO;
using System.Net.Http.Json;
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
            var cabs = await AppSettings.Api.Client.GetFromJsonAsync<List<CabinetDTO>>(AppSettings.Api.ApiRequestUrl(ApiRequestType.Cabinet, "all")); //"api/Cabinet/all");

            return View(cabs);
        }

        //Action для загрузки страницы с информацией об кабинете
        [HttpGet("cabinets/cabientId={id}")]
        [Authorize]
        public async Task<ActionResult> CabInfo([FromRoute] int id)
        {
            //Cabinet? cabinet = await apiHttpClient.GetFromJsonAsync<Cabinet>($"api/Cabinet/get/id={id}");

            //Сделать запрос к списку оборудования

            return View(await GetCabinetInfo(id));
        }

        //Action для поиска оборудования в кабинете
        [HttpPost("equip-search-{id}")]
        [Authorize]
        public async Task<ActionResult> SearchEquipment(string searchField, [FromRoute] int id)
        {
            var cabinetInfo = await GetCabinetInfo(id);
            cabinetInfo.SelectedList = "Equipment";

            /*Equipment[] copy = new Equipment[tableData.Count];
            tableData.CopyTo(copy, 0);

            var searched = copy.ToList();

            searchField ??= string.Empty;*/

            if (searchField.Length > 0)
            {
                if (cabinetInfo.SelectedList == "Equipment")
                    cabinetInfo.Equipments = cabinetInfo.Equipments.Where(c =>
                        c.Name.Contains(searchField) ||
                        c.Description.Contains(searchField)
                    ).ToList();
            }

            return PartialView("CabInfo/_TableTemplatePartial", cabinetInfo);
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

        [HttpPost("send-edit-data-equipment")]
        [Authorize]
        public async Task<ActionResult> SendEditedEquipment([FromForm] EquipmentDTO equipment, [FromForm] int EquipmentType) 
        {
            equipment.EquipmentType = new()
            {
                Id = EquipmentType
            };

            Console.WriteLine($"" +
               $"{equipment.Id}\n" +
               $"{equipment.Name}\n" +
               $"{equipment.Description}\n" +
               $"{equipment.InventoryNumber}\n" +
               $"{equipment.EquipmentType.Id}");

            return Ok();
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
