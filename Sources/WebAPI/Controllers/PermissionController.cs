using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.Services.PermissionService;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController(IPermissionService permissionService) : Controller
    {
        private readonly IPermissionService _permissionService = permissionService;

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPrimissions() 
        {
            var primissions = (await _permissionService.GetPermissions()).ToList();

            if (primissions.Count == 0)
                return NotFound("В БД нет записей с привилегиями");

            return Ok(primissions);
        }

        [HttpGet("get/name={name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name) 
        {
            var premission = await _permissionService.GetPermission(name);

            if (premission == null)
                return NotFound("Привилегия не найдена");

            return Ok(premission);
        }

        [HttpGet("get/id={id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var premission = await _permissionService.GetPermission(id);

            if (premission == null)
                return NotFound("Привилегия не найдена");

            return Ok(premission);
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddUserPremission([FromBody] PermissionDTO newPermission)
        {
            var premission = await _permissionService.AddPermission(newPermission);

            if (premission == null)
                return BadRequest("Привелегия пользователя уже существует");

            return Ok("Привилегия добавлена в БД");
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePrimission([FromHeader] int id)
        {
            var permission = await _permissionService.DeletePermission(id);

            if (!permission)
                return BadRequest($"Привилегия с id '{id}' не найдена");

            return Ok("Привилегия удалена из БД");
        }
    }
}
