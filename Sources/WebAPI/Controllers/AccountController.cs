using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebAPI.DataContext;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(DB_Context context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllState() 
        {
            var state = await context.AccountStats.ToListAsync();

            return Ok(state);
        }

        [HttpGet("stateAccId")]
        public async Task<IActionResult> GetAccountState([FromHeader] int accId) 
        {
            var state = await context.AccountStats
                .Where(acc => acc.UserId == accId)
                .FirstOrDefaultAsync();

            if (state == null)
                return BadRequest("Ключ доступа у пользователя не найден");

            if(state.ExpirationKeyDate > DateTime.Now)
            {
                context.AccountStats.Remove(state);
                await context.SaveChangesAsync();

                return BadRequest("Истёк срок давности ключа доступа");
            }

            return Ok();
        }
    }
}
