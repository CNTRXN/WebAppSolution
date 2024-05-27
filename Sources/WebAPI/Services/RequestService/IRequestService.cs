using ModelLib.Model;
using ModelLib.DTO;

namespace WebAPI.Services.RequestService
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>?> GetAllRequests();
        Task<RequestDTO?> GetRequestBy_Id(int requestId);
        Task<IEnumerable<RequestDTO>?> GetRequestsBy_UserId(int userId);
        Task<IEnumerable<RequestDTO>?> GetRequestsBy_CabinetId(int cabinetId);
        Task<IEnumerable<RequestDTO>?> GetRequestsBy_StatusId(int statusId);
        Task<IEnumerable<RequestDTO>?> GetRequestsBy_TypeId(int typeId);
        Task<IEnumerable<RequestDTO>?> GetRequestsBy_StatusId_And_TypeId(int statusId, int typeId);
        Task<IEnumerable<RequestDTO>?> GetRequestsBy_CabinetId_And_StatusId(int cabinetId, int statusId);
        Task<IEnumerable<RequestDTO>?> GetRequestsBy_CabinetId_And_TypeId(int cabinetId, int typeId);
        Task<IEnumerable<RequestDTO>?> GetRequestsBy_CabinetId_And_StatusId_And_TypeId(int cabinetId, int statusId, int typeId);

        Task<Request?> AddRepairRequest(int cabinetId, int? userId, List<int> equipmentsIds, string Title, string Description, List<IFormFile> images);
    }
}
