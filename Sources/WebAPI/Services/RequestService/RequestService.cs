using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.Services.RequestService;

namespace WebAPI.Services.RequestService
{
    public class RequestService(DB_Context context) : IRequestService
    {
        public async Task<bool> AddRepairRequest(int cabinetId, int? userId, List<int> equipmentsIds, string Title, string Description, List<IFormFile> images)
        {
            #region Проверка на существование
            var cabinet = await context.Cabinets.Where(c => c.Id == cabinetId).FirstOrDefaultAsync();

            if (cabinet == null)
                return false;



            #endregion

            return true;
        }

        public async Task<Request> GetAllRequests()
        {
            throw new NotImplementedException();
        }

        public async Task<RequestDTO> GetRequestByCabinetId(int cabinetId)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestDTO> GetRequestById(int requestId)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestDTO> GetRequestByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
