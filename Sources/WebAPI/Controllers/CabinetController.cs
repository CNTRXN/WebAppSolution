using Microsoft.AspNetCore.Mvc;
using WebAPI.DataContext.DTO;
using WebAPI.Models;
using WebAPI.Services.CabinetService;
using WebAPI.Services.FileService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetController(ICabinetService cabinetService, IFileService fileService) : Controller
    {
        //Контроллер для получения всех записей кабинета
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCabinets() 
        {
            var cabinets = (await cabinetService.GetCabinets()).ToList();

            if (cabinets.Count == 0)
                return NotFound("В БД нет записей с кабинетами");

            return Ok(cabinets);
        }

        //Контроллер для получения кабинета по id записи
        [HttpGet("get/id={id}")]
        public async Task<IActionResult> GetCabinetById([FromRoute] int id)
        {
            var cabinet = await cabinetService.GetCabinet(id);

            if (cabinet == null)
                return NotFound($"В БД нет записи кабинета с id '{id}'");

            return Ok(cabinet);
        }

        //Получение оборудования кабинета по id
        [HttpGet("get-equip/id={id}")]
        public async Task<IActionResult> GetCabinetEquipments([FromRoute] int id) 
        {
            var equipments = await cabinetService.GetCabinetEquipments(id);

            if (equipments == null)
                return NotFound($"В БД нет записей с оборудованием в кабинете с id '{id}'");

            return Ok(equipments);
        }

        //Добавление оборудования к кабинету
        [HttpPost("add-equip-to-cab/cabid={cabId}")]
        public async Task<IActionResult> AddEquipmentsToCabinet([FromRoute] int cabId, [FromBody] List<AddEquipToCabDTO> equipIdAndCount) 
        {
            var cabinet = await cabinetService.GetCabinet(cabId);

            if(cabinet == null)
                return BadRequest($"Кабинет с id '{cabId}' не найден");

            var addedRows = await cabinetService.AddEquipmentsToCabinet(cabinet.Id, equipIdAndCount);

            if (addedRows == null)
                return BadRequest($"Не удалось добавить '{equipIdAndCount.Count}' оборудования к кабинету");

            return Ok($"'{addedRows}' строчек добавлено к кабинету");
        }

        //Добавление нового кабинета
        [HttpPost("new")]
        public async Task<IActionResult> AddNewCabinet([FromBody] NewCabinetDTO newCabinet) 
        {
            var cabinet = await cabinetService.AddCabinet(newCabinet);

            if (cabinet == null)
                return BadRequest("Кабинет уже существует");

            return Ok(cabinet);
        }

        //Удаление кабинета по id
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCabinet([FromHeader] int id) 
        {
            var cabinet = await cabinetService.DeleteCabinet(id);

            if (!cabinet)
                return BadRequest($"Кабинета с '{id}' не существует");

            return Ok("Кабинет удалён");
        }

        #region Изображения и файлы
        [HttpPost("file/uploadFile"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddNewCabinetFile([FromForm] FileUploadModel file, int cabinetId)
        {
            var newCabinetPhoto = await fileService.AddNewCabinetFile(file, cabinetId);

            if (!newCabinetPhoto)
                return BadRequest();

            return Ok();
        }

        [HttpPost("file/uploadFiles"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddNewCabinetFiles([FromForm] List<FileUploadModel> file, int cabinetId)
        {
            var newCabinetPhoto = await fileService.AddNewCabinetFiles(file, cabinetId);

            if (!newCabinetPhoto)
                return BadRequest();

            return Ok();
        }

        [HttpPost("image/uploadPhoto"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddNewCabinetPhoto([FromForm] FileUploadModel image, int cabinetId)
        {
            var newCabinetPhoto = await fileService.AddNewCabinetImage(image, cabinetId);

            if (!newCabinetPhoto)
                return BadRequest();

            return Ok();
        }

        [HttpPost("image/uploadPhotos"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddNewCabinetPhotos([FromForm] List<FileUploadModel> images, int cabinetId)
        {
            var newCabinetPhoto = await fileService.AddNewCabinetImages(images, cabinetId);

            if (!newCabinetPhoto)
                return BadRequest();

            return Ok();
        }
        #endregion
    }
}
