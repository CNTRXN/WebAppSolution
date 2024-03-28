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
        Cabinet? _cabinet;

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
        [Authorize(Roles = "Пользователь")]
        public async Task<IActionResult> CabList()
        {
            var cabs = await apiHttpClient.GetFromJsonAsync<List<CabinetDTO>>("api/Cabinet/all");

            return View(cabs);
        }

        //Action для загрузки страницы оборудования кабинета
        [HttpGet("cabinets/cabientId={id}")]
        [Authorize(Roles = "Пользователь")]
        public async Task<ActionResult> CabInfo([FromRoute] int id)
        {
            _cabinet = await apiHttpClient.GetFromJsonAsync<Cabinet>($"api/Cabinet/get/id={id}");

            //Сделать запрос к списку оборудования

            return View(new CabInfoPage()
            {
                Equipments = tableData,
                Cabinet = _cabinet ?? new()
            });
        }

        //Action для поиска оборудования в кабинете
        [HttpPost("equip-search")]
        [Authorize(Roles = "Пользователь")]
        public async Task<ActionResult> SearchEquipment(string searchField)
        {
            Equipment[] copy = new Equipment[tableData.Count];
            tableData.CopyTo(copy, 0);

            var searched = copy.ToList();

            searchField ??= string.Empty;

            if (searchField.Length > 0)
            {
                searched = searched.Where(c =>
                    c.Name.Contains(searchField) ||
                    c.Description.Contains(searchField)
                ).ToList();
            }

            return View(new CabInfoPage()
            {
                Equipments = searched,
                SearchQuery = searchField,
                Cabinet = _cabinet ?? new()
            });
        }
    }
}
