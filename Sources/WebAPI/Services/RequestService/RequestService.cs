using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.Models;
using WebAPI.Services.FileService;
using WebAPI.Services.RequestService;

namespace WebAPI.Services.RequestService
{
    public class RequestService(DB_Context context, IFileService fileService) : IRequestService
    {
        public async Task<bool> AddRepairRequest(int cabinetId, int? userId, List<int> equipmentsIds, string Title, string Description, List<IFormFile> images)
        {
            #region Проверка на существование
            //проверка на существование кабинета
            var cabinet = await context.Cabinets.Where(c => c.Id == cabinetId).FirstOrDefaultAsync();

            if (cabinet == null)
                return false;

            //проверка на существование оборудования
            List<Equipment> equipments = [];
            foreach (var equipmentsId in equipmentsIds) 
            {
                var equipment = await context.Equipments
                    .Where(e => e.Id == equipmentsId)
                    .FirstOrDefaultAsync();

                if (equipment != null)
                    equipments.Add(equipment);
            }

            /*
             Если пользователь введён, то проверяем его на существование
             */
            User? user = null;
            if(userId != null)
                user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            #endregion

            //Если пустое название заявки
            if (Title.Length == 0)
                Title = $"new_request_{DateTime.Now:s}";

            //Создание заявки
            Request request = new()
            {
                Title = Title,
                Description = Description,
                CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                RequestStatusId = 1,
                RequestTypeId = 1,
                CabId = cabinet.Id,
                FromId = user?.Id
            };

            await context.Requests.AddAsync(request);
            await context.SaveChangesAsync();

            //Прикрепление оборудования
            await context.RequestEquipments
                .AddRangeAsync(equipments.Select(e => 
                new RequestEquipment() 
                {
                    RequestId = request.Id,
                    EquipmentId = e.Id
                }).ToList());
            await context.SaveChangesAsync();

            //Прикрепление фотографий
            if (images.Count > 0)
            {
                List<FileUploadModel> fileOnUpload = [];

                foreach (var image in images) 
                {
                    fileOnUpload.Add(new FileUploadModel() 
                    {
                        File = image,
                        FileAuthorId = user?.Id
                    });
                }

                if (!await fileService.AddNewRequestImages(fileOnUpload, request.Id))
                    return false;
            }

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
