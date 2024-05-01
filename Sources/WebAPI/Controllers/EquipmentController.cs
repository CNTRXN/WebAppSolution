using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController(IEquipmentService equipmentService) : Controller
    {
        private readonly IEquipmentService _equipmentService = equipmentService;

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEquipment()
        {
            var equipments = await _equipmentService.GetEquipments();

            if (equipments == null)
                return BadRequest("В базе данных нет записей с оборудованием");

            return Ok(equipments);
        }

        [HttpGet("get/id={id}")]
        public async Task<IActionResult> GetEquipmentById([FromRoute] int id)
        {
            var equipment = await _equipmentService.GetEquipment(id);

            if (equipment == null)
                return BadRequest($"Оборудование с id '{id}' не найден");

            return Ok(equipment);
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddNewEquipments([FromBody] List<NewEquipmentDTO> equipments) 
        {
            var addedCount = await _equipmentService.AddNewEquipments(equipments);

            if (addedCount == null)
                return BadRequest("Не удалось добавить записи");

            return Ok($"Добавлено {addedCount} записи");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEquipment([FromBody] List<EquipmentDTO> equipments)
        {
            var deletedEquipments = await _equipmentService.DeleteEquipments(equipments);

            if (deletedEquipments == null)
                return BadRequest("Не удалось удалить записи");

            return Ok($"Удалено {deletedEquipments} записи");
        }
    }
}
