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
        [HttpPost("repair")]
        public async Task<IActionResult> AddNewRepairRequest(
            [FromForm] int cabinetId, 
            [FromForm] int? userId, 
            [FromForm] string Title, 
            [FromForm] string Description, 
            [FromForm] List<int> equipmentsIds, 
            [FromForm] List<IFormFile> images) 
        {
            /*var request = await requestService.AddRepairRequest(cabinetId, userId, equipmentsIds, Title, Description, images);

            if (!request)
                return BadRequest();*/

            Console.WriteLine($"{cabinetId}");
            Console.WriteLine($"{userId}");
            Console.WriteLine($"{Title}");
            Console.WriteLine($"{Description}");
            equipmentsIds.ForEach(e => 
            {
                Console.WriteLine($"{e}");
            });
            images.ForEach(e =>
            {
                Console.WriteLine($"{e.FileName}");
            });

            return Ok();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllRequests() 
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetRequestById()
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetRequestByCabinetId()
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddNewRequest() 
        //{
        //    return Ok();
        //}
    }
}
