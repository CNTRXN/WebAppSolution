using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.RequestService;
using WebAPI.Services.RequestStatusesService;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestStatusController(IRequestStatusService requestStatusesService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRequestsStatuses()
        {
            return Ok(await requestStatusesService.GetAllRequestsStatuses());
        }
    }
}
