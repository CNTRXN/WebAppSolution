using Microsoft.AspNetCore.Mvc;
using ModelLib.Model;
using ModelLib.DTO;
using WebAPI.Services.EquipmentService;
using WebAPI.Services.Notification;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController(IEquipmentService equipmentService, IHubContext<NotificationService> hubContext) : ControllerBase
    {

        [HttpGet("all")]
        public async Task<IActionResult> GetAllEquipment()
        {
            var equipments = await equipmentService.GetEquipments();

            if (equipments == null)
                return BadRequest("В базе данных нет записей с оборудованием");

            return Ok(equipments);
        }

        [HttpGet("get/id={id}")]
        public async Task<IActionResult> GetEquipmentById([FromRoute] int id)
        {
            var equipment = await equipmentService.GetEquipment(id);

            if (equipment == null)
                return BadRequest($"Оборудование с id '{id}' не найден");

            return Ok(equipment);
        }

        [HttpGet("get/byids")]
        public async Task<IActionResult> GetEquipmentsById([FromQuery] List<int> ids) 
        {
            var equipments = await equipmentService.GetEquipmentsById(ids);

            if (equipments == null)
                return BadRequest();

            return Ok(equipments);
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddNewEquipments([FromBody] List<NewEquipmentDTO> equipments) 
        {
            var addedCount = await equipmentService.AddNewEquipments(equipments);

            if (addedCount == null)
                return BadRequest("Не удалось добавить записи");

            var senderId = HttpContext.Request.Cookies["sender-id"];
            var connectionId = NotificationService.ConnectedUsers
                .Where(cu => cu.UserId == senderId)
                .Select(cu => cu.ConnectionId)
                .FirstOrDefault();

            if (connectionId != null)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("UpdatePage");
            }

            return Ok($"Добавлено {addedCount} записи");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEquipment([FromBody] List<EquipmentDTO> equipments)
        {
            var deletedEquipments = await equipmentService.DeleteEquipments(equipments);

            if (deletedEquipments == null)
                return BadRequest("Не удалось удалить записи");

            var senderId = HttpContext.Request.Cookies["sender-id"];
            var connectionId = NotificationService.ConnectedUsers
                .Where(cu => cu.UserId == senderId)
                .Select(cu => cu.ConnectionId)
                .FirstOrDefault();

            if (connectionId != null)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("UpdatePage");
            }

            return Ok($"Удалено {deletedEquipments} записи");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEquipment([FromHeader] int id, [FromBody] NewEquipmentDTO updateEquipment) 
        {
            var updateEquipmentResult = await equipmentService.UpdateEquipment(id, updateEquipment);

            if (!updateEquipmentResult)
                return BadRequest("Не удалось обновить запись");

            var senderId = HttpContext.Request.Cookies["sender-id"];
            var connectionId = NotificationService.ConnectedUsers
                .Where(cu => cu.UserId == senderId)
                .Select(cu => cu.ConnectionId)
                .FirstOrDefault();

            if (connectionId != null)
            {
                await hubContext.Clients.Client(connectionId).SendAsync("UpdatePage");
            }

            //ApiDebug.ConsoleOutput(updateEquipment);

            return Ok($"Запис успешно обновлена");
        }
    }
}
