using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.RequestService
{
    public interface IRequestService
    {
        //get all
        Task<Request> GetAllRequests();
        //get by id
        Task<RequestDTO> GetRequestById(int requestId);
        //get by from user id
        Task<RequestDTO> GetRequestByUserId(int userId);
        //get by cabinet id
        Task<RequestDTO> GetRequestByCabinetId(int cabinetId);
        //get by request type


        //add repair request
        Task<bool> AddRepairRequest(int cabinetId, int? userId, List<int> equipmentsIds, string Title, string Description, List<IFormFile> images);

        //delete by id
        //add
        //update status
    }
}
