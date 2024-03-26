using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.Other;
using WebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController(ICabinetService cabinetService) : Controller
    {
        private readonly ICabinetService _cabinetService = cabinetService;

        //Контроллер для получения всех записей кабинета
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCabinets() 
        {
            var cabinets = (await _cabinetService.GetCabinets()).ToList();

            if (cabinets.Count == 0)
                return NotFound("В БД нет записей с кабинетами");

            return Ok(cabinets);
        }

        //Контроллер для получения кабинета по id записи
        [HttpGet("get/id={id}")]
        public async Task<IActionResult> GetCabinetById([FromRoute] int id)
        {
            var cabinet = await _cabinetService.GetCabinet(id);

            if (cabinet == null)
                return NotFound($"В БД нет записи кабинета с id '{id}'");

            return Ok(cabinet);
        }

        //Добавление нового кабинета
        [HttpPost("new")]
        public async Task<IActionResult> AddNewCabinet([FromBody] CabinetDTO newCabinet) 
        {
            var cabinet = await _cabinetService.AddCabinet(newCabinet);

            if (cabinet == null)
                return BadRequest("Кабинет уже существует");

            return Ok(cabinet);
        }

        //Удаление кабинета по id
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCabinet([FromHeader] int id) 
        {
            var cabinet = await _cabinetService.DeleteCabinet(id);

            if (!cabinet)
                return BadRequest($"Кабинета с '{id}' не существует");

            return Ok("Кабинет удалён");
        }
    }
}
