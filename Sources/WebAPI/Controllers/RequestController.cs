﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services.FileService;
using WebAPI.Services.RequestService;
using ModelLib.DTO;
using ModelLib.Model;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Services.Notification;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController(IRequestService requestService, IHubContext<NotificationService> hubContext) : ControllerBase
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
            var request = await requestService.AddRepairRequest(new NewRequestDTO() 
            {
                CabinetId = cabinetId, 
                UserId = userId, 
                EquipmentsIds = equipmentsIds, 
                Title = Title, 
                Description = Description, 
                Images = images
            });

            if (request == null)
                return BadRequest();

            return Ok();
        }

        //Обновление заявки
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRequest([FromHeader] int id, [FromBody] Request requestNewData) 
        {
            var request = await requestService.UpdateRequest(id, requestNewData);

            if (!request)
                return BadRequest();

            //var senderId = HttpContext.Request.Cookies["sender-id"];

            return Ok();
        }
    }
}