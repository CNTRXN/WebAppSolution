using ModelLib.Model;
using WebAPI.DataContext;
using Microsoft.EntityFrameworkCore;
using WebAPI.Services.RequestStatusesService;

namespace WebAPI.Services.RequestStatusesService
{
    public class RequestStatusService(DB_Context context) : IRequestStatusService
    {
        public async Task<IEnumerable<RequestStatus>> GetAllRequestsStatuses()
        {
            return await context.RequestStatuses.ToListAsync();
        }
    }
}
