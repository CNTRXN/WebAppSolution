using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Models.DTO;
using WebApp.Models.EquipData;
using WebApp.Settings;

namespace WebApp.Controllers
{
    public class DataController : Controller
    {
        readonly List<Equipment> tableData = [];
        //Cabinet? _cabinet;

        private HttpClient apiHttpClient = new()
        {
            BaseAddress = new Uri(AppStatics.ApiUrl)
        };

        public DataController()
        {
            for (int i = 0; i < 20; i++)
            {
                tableData.Add(new Equipment()
                {
                    Id = i,
                    Count = new Random().Next(0, 100),
                    Description = $"Table-{i}",
                    Name = $"Name-{i}",
                    TypeId = new Random().Next(0, 4)
                });
            }
        }
        //Action для загрузки страницы кабинетов
        [HttpGet("cabinets")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> CabList()
        {
            var cabs = await apiHttpClient.GetFromJsonAsync<List<CabinetDTO>>("api/Cabinet/all");

            return View(cabs);
        }

        //Action для загрузки страницы оборудования кабинета
        [HttpGet("cabinets/cabientId={id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> CabInfo([FromRoute] int id)
        {
            //Cabinet? cabinet = await apiHttpClient.GetFromJsonAsync<Cabinet>($"api/Cabinet/get/id={id}");

            //Сделать запрос к списку оборудования

            return View(await GetCabinetInfo(id));
        }

        //Action для поиска оборудования в кабинете
        [HttpPost("equip-search-{id}")]
        [Authorize(Roles = "User")]
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

            return PartialView("_TableTemplatePartial", cabinetInfo);
        }

        [HttpGet("show-equipments")]
        public async Task<ActionResult> ShowEquipments([FromHeader] int cabId)
        {
            List<EquipmentDTO>? equipment = await apiHttpClient.GetFromJsonAsync<List<EquipmentDTO>>($"api/Cabinet/get-equip/id={cabId}");

            return PartialView("_TableTemplatePartial", new CabInfoPage()
            {
                Equipments = equipment.Cast<dynamic>().ToList() ?? [],
                SelectedList = "Equipment"
            });
        }

        [HttpGet("show-test")]
        public async Task<ActionResult> ShowTestTable() 
        {
            var testTable = new List<TestData>() 
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

            return PartialView("_TableTemplatePartial", new CabInfoPage()
            {
                Equipments = testTable.Cast<dynamic>().ToList() ?? [],
                SelectedList = "TestData"
            });
        }

        [HttpGet("open-image-form")]
        public async Task<ActionResult> ShowCabinetsPhoto([FromHeader] int cabId) 
        {
            List<string>? imageHref = await apiHttpClient.GetFromJsonAsync<List<string>>($"api/File/images/cabId={cabId}");

            return PartialView("_CabinetImageFormPartial", imageHref);
        }

        private async Task<CabInfoPage> GetCabinetInfo(int cabId) 
        {
            Cabinet? cabinet = await apiHttpClient.GetFromJsonAsync<Cabinet>($"api/Cabinet/get/id={cabId}");
            //List<EquipmentDTO>? equipment = await apiHttpClient.GetFromJsonAsync<List<EquipmentDTO>>($"api/Cabinet/get-equip/id={cabId}");

            return new CabInfoPage()
            {
                Cabinet = cabinet ?? new(),
                //Equipments = equipment.Cast<dynamic>().ToList()
            };
        }
    }
}
