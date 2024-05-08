using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.RequestService
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>> GetAllRequests();
        Task<RequestDTO> GetRequestById(int requestId);
        Task<IEnumerable<RequestDTO>> GetRequestsByUserId(int userId);
        Task<IEnumerable<RequestDTO>> GetRequestsByCabinetId(int cabinetId);

        //add repair request
        Task<bool> AddRepairRequest(int cabinetId, int? userId, List<int> equipmentsIds, string Title, string Description, List<IFormFile> images);
        
        //delete by id
        //update status
    }
}
