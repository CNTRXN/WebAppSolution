using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.EncryptingData;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService)
        : Controller
    {
        private readonly IUserService _userService = userService;

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = (await _userService.GetUsers()).ToList();

            if (users.Count == 0)
                return BadRequest("В БД нет записей пользователей");

            return Ok(users);
        }

        [HttpGet("get/id={id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var foundedUser = await _userService.GetUser(id);

            if (foundedUser == null)
                return BadRequest($"Пользователь с id '{id}' не найден");

            return Ok(foundedUser);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetUserByLoginAndPassword([FromHeader] string login, [FromHeader] string password)
        {
            var foundedUser = await _userService.GetUser(login, password);

            if (foundedUser == null)
                return BadRequest("Пользователь не найден");

            return Ok(foundedUser);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> AddUser([FromBody] NewUserDTO newUser) 
        {
            var addedUser = await _userService.AddUser(newUser);

            if (addedUser == null)
                return BadRequest("Не удалось добавить пользователя!");

            return Ok(addedUser);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser([FromHeader] int id)
        {
            var deletedUser = await _userService.DeleteUser(id);

            if (!deletedUser)
                return BadRequest("Не удалось удалить пользователя");

            return Ok($"Пользователь с id '{id}' удалён");
        }

        /*[HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] NewUserDTO user)
        {
            var userOnChange = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            if (userOnChange == null)
                return NotFound($"Пользователь с id '{id}' не найден");
            else 
            {

            }
        }*/
    }
}