﻿using Microsoft.AspNetCore.Mvc;
using ModelLib.Model;
using ModelLib.DTO;
using WebAPI.Services.EquipmentService;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController(IEquipmentService equipmentService) : ControllerBase
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

        [HttpGet("get/byids")]
        public async Task<IActionResult> GetEquipmentsById([FromQuery] List<int> ids) 
        {
            var equipments = await _equipmentService.GetEquipmentsById(ids);

            if (equipments == null)
                return BadRequest();

            return Ok(equipments);
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
