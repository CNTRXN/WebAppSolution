using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.FileService;
using WebAPI.Services.RequestService;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController(IRequestService requestService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllRequests() 
        {
            return Ok(await requestService.GetAllRequests());
        }

        [HttpGet("requests/cabid={cabid}")]
        public async Task<IActionResult> GetRequestByCabinetId([FromRoute] int cabid)
        {
            var requests = await requestService.GetRequestsBy_CabinetId(cabid);

            if (requests == null)
                return BadRequest();

            return Ok(requests);
        }

        [HttpPost("repair")]
        public async Task<IActionResult> AddNewRepairRequest(
            [FromForm] int cabinetId,
            [FromForm] int? userId,
            [FromForm] string Title,
            [FromForm] string Description,
            [FromForm] List<int> equipmentsIds,
            [FromForm] List<IFormFile> images)
        {
            var request = await requestService.AddRepairRequest(cabinetId, userId, equipmentsIds, Title, Description, images);

            if (request == null)
                return BadRequest();

            return Ok();
        }
    }
}
