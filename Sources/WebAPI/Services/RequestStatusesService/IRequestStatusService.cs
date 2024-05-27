using ModelLib.Model;

namespace WebAPI.Services.RequestStatusesService
{
    public interface IRequestStatusService
    {
        Task<IEnumerable<RequestStatus>> GetAllRequestsStatuses();
    }
}
