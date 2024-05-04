using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.EquipmentService;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTypeController(IEquipmentService equipmentService) : Controller
    {
        private readonly IEquipmentService _equipmentService = equipmentService;

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEquipmentType() 
        {
            var equipmentsType = await _equipmentService.GetEquipmentTypes();

            if (equipmentsType == null)
                return BadRequest("В базе данных нет записей с типами оборудования");

            return Ok(equipmentsType);
        }

        [HttpGet("get/id={id}")]
        public async Task<IActionResult> GetEquipmentType([FromRoute] int id) 
        {
            var equipmentType = await _equipmentService.GetEquipment(id);

            if (equipmentType == null)
                return BadRequest($"В базе данных нет записей с '{id}' типа оборудования");

            return Ok(equipmentType);
        }

        [HttpPost("new-equipmentT")]
        public async Task<IActionResult> AddNewEquipmentType(string typeName)
        {
            var equipmentType = await _equipmentService.AddNewEquipmentType(typeName);

            if (equipmentType)
                return BadRequest($"Не удалось добавить тип оборудования '{typeName}'");

            return Ok($"Тип оборудования '{typeName}' успешно добавлен");
        }
    }
}
