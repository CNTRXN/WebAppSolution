using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using ModelLib.Model;
using ModelLib.DTO;
using WebAPI.Services.FileService;

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

        public async Task<IEnumerable<Request>?> GetAllRequests()
        {
            return await context.Requests.ToListAsync();
        }

        [Obsolete("Не доделан")]
        public async Task<RequestDTO?> GetRequestBy_Id(int requestId)
        {
            //!!!!!!!!!
            var rawRequest = await context.Requests
                .Where(r => r.Id == requestId)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO()
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            //Group = c.Group,
                            Height = c.Height,
                            Length = c.Length,
                            Num = c.Num,
                            PlanNum = c.PlanNum,
                            Width = c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == c.ResponsiblePersonId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    Surname = u.Surname,
                                    PermissionName = context.Permissions
                                        .Where(p => p.Id == u.PermissionId)
                                        .Select(p => p.Name)
                                        .First()
                                })
                                .First()
                        })
                        .First(),
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            Surname = u.Surname,
                            PermissionName = context.Permissions
                                        .Where(p => p.Id == u.PermissionId)
                                        .Select(p => p.Name)
                                        .First()
                        })
                        .FirstOrDefault(),
                    CompleteDate = r.CompleteDate,
                    CreatedDate = r.CreatedDate,
                    Description = r.Description,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    Title = r.Title
                })
                .FirstOrDefaultAsync();

            RequestDTO request = new();

            //прикрепить изображение

            if (request == null)
                return null;

            return request;
        }

        public async Task<IEnumerable<RequestDTO>?> GetRequestsBy_CabinetId(int cabinetId)
        {
            var cabinet = await context.Cabinets.FirstOrDefaultAsync(c => c.Id == cabinetId);

            if (cabinet == null)
                return null;

            var requests = await context.Requests
                .Where(r => r.CabId == cabinet.Id)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    CompleteDate = r.CompleteDate,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO() 
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                            Surname = u.Surname
                        })
                        .FirstOrDefault(),
                    Images = context.RequestFiles
                        .Where(rf => rf.RequestId == r.Id)
                        .Select(rf => rf.FilePath)
                        .ToList(),
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO() 
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            Height = c.Height,
                            Length= c.Length,
                            Num= c.Num,
                            PlanNum= c.PlanNum,
                            Width= c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == r.FromId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                                    Surname = u.Surname
                                })
                                .FirstOrDefault()
                        })
                        .First()

                })
                .ToListAsync();

            return requests;
        }


        public async Task<IEnumerable<RequestDTO>?> GetRequestsBy_CabinetId_And_StatusId_And_TypeId(int cabinetId, int statusId, int typeId)
        {
            var cabinet = await context.Cabinets.FirstOrDefaultAsync(c => c.Id == cabinetId);

            if (cabinet == null)
                return null;

            var status = await context.RequestStatuses.FirstOrDefaultAsync(r => r.Id == statusId);

            if (status == null)
                return null;

            var type = await context.RequestTypes.FirstOrDefaultAsync(rt => rt.Id == typeId);

            if (type == null)
                return null;

            var requests = await context.Requests
                .Where(r => r.CabId == cabinet.Id && r.RequestTypeId == type.Id && r.RequestStatusId == status.Id)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    CompleteDate = r.CompleteDate,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                            Surname = u.Surname
                        })
                        .FirstOrDefault(),
                    Images = context.RequestFiles
                        .Where(rf => rf.RequestId == r.Id)
                        .Select(rf => rf.FilePath)
                        .ToList(),
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO()
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            Height = c.Height,
                            Length = c.Length,
                            Num = c.Num,
                            PlanNum = c.PlanNum,
                            Width = c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == r.FromId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                                    Surname = u.Surname
                                })
                                .FirstOrDefault()
                        })
                        .First()

                })
                .ToListAsync();

            return requests;
        }

        public async Task<IEnumerable<RequestDTO>?> GetRequestsBy_CabinetId_And_StatusId(int cabinetId, int statusId)
        {
            var cabinet = await context.Cabinets.FirstOrDefaultAsync(c => c.Id == cabinetId);

            if (cabinet == null)
                return null;

            var status = await context.RequestStatuses.FirstOrDefaultAsync(r => r.Id == statusId);

            if (status == null)
                return null;

            var requests = await context.Requests
                .Where(r => r.RequestStatusId == status.Id && r.CabId == cabinet.Id)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    CompleteDate = r.CompleteDate,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                            Surname = u.Surname
                        })
                        .FirstOrDefault(),
                    Images = context.RequestFiles
                        .Where(rf => rf.RequestId == r.Id)
                        .Select(rf => rf.FilePath)
                        .ToList(),
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO()
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            Height = c.Height,
                            Length = c.Length,
                            Num = c.Num,
                            PlanNum = c.PlanNum,
                            Width = c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == r.FromId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                                    Surname = u.Surname
                                })
                                .FirstOrDefault()
                        })
                        .First()

                })
                .ToListAsync();

            return requests;
        }

        public async Task<IEnumerable<RequestDTO>?> GetRequestsBy_CabinetId_And_TypeId(int cabinetId, int typeId)
        {
            var cabinet = await context.Cabinets.FirstOrDefaultAsync(c => c.Id == cabinetId);

            if (cabinet == null)
                return null;

            var type = await context.RequestTypes.FirstOrDefaultAsync(rt => rt.Id == typeId);

            if (type == null)
                return null;

            var requests = await context.Requests
                .Where(r => r.RequestTypeId == type.Id && r.CabId == cabinet.Id)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    CompleteDate = r.CompleteDate,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                            Surname = u.Surname
                        })
                        .FirstOrDefault(),
                    Images = context.RequestFiles
                        .Where(rf => rf.RequestId == r.Id)
                        .Select(rf => rf.FilePath)
                        .ToList(),
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO()
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            Height = c.Height,
                            Length = c.Length,
                            Num = c.Num,
                            PlanNum = c.PlanNum,
                            Width = c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == r.FromId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                                    Surname = u.Surname
                                })
                                .FirstOrDefault()
                        })
                        .First()

                })
                .ToListAsync();

            return requests;
        }

        public async Task<IEnumerable<RequestDTO>?> GetRequestsBy_StatusId_And_TypeId(int statusId, int typeId)
        {
            var status = await context.RequestStatuses.FirstOrDefaultAsync(r => r.Id == statusId);

            if (status == null)
                return null;

            var type = await context.RequestTypes.FirstOrDefaultAsync(rt => rt.Id == typeId);

            if (type == null)
                return null;

            var requests = await context.Requests
                .Where(r => r.RequestStatusId == status.Id && r.RequestTypeId == type.Id)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    CompleteDate = r.CompleteDate,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                            Surname = u.Surname
                        })
                        .FirstOrDefault(),
                    Images = context.RequestFiles
                        .Where(rf => rf.RequestId == r.Id)
                        .Select(rf => rf.FilePath)
                        .ToList(),
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO()
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            Height = c.Height,
                            Length = c.Length,
                            Num = c.Num,
                            PlanNum = c.PlanNum,
                            Width = c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == r.FromId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                                    Surname = u.Surname
                                })
                                .FirstOrDefault()
                        })
                        .First()

                })
                .ToListAsync();

            return requests;
        }
        
        public async Task<IEnumerable<RequestDTO>?> GetRequestsBy_StatusId(int statusId)
        {
            var status = await context.RequestStatuses.FirstOrDefaultAsync(r => r.Id == statusId);

            if (status == null)
                return null;

            var requests = await context.Requests
                .Where(r => r.RequestStatusId == status.Id)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    CompleteDate = r.CompleteDate,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                            Surname = u.Surname
                        })
                        .FirstOrDefault(),
                    Images = context.RequestFiles
                        .Where(rf => rf.RequestId == r.Id)
                        .Select(rf => rf.FilePath)
                        .ToList(),
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO()
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            Height = c.Height,
                            Length = c.Length,
                            Num = c.Num,
                            PlanNum = c.PlanNum,
                            Width = c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == r.FromId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                                    Surname = u.Surname
                                })
                                .FirstOrDefault()
                        })
                        .First()

                })
                .ToListAsync();

            return requests;
        }

        public async Task<IEnumerable<RequestDTO>?> GetRequestsBy_TypeId(int typeId)
        {
            var type = await context.RequestTypes.FirstOrDefaultAsync(rt => rt.Id == typeId);

            if (type == null)
                return null;

            var requests = await context.Requests
                .Where(r => r.RequestTypeId == type.Id)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    CompleteDate = r.CompleteDate,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                            Surname = u.Surname
                        })
                        .FirstOrDefault(),
                    Images = context.RequestFiles
                        .Where(rf => rf.RequestId == r.Id)
                        .Select(rf => rf.FilePath)
                        .ToList(),
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO()
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            Height = c.Height,
                            Length = c.Length,
                            Num = c.Num,
                            PlanNum = c.PlanNum,
                            Width = c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == r.FromId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                                    Surname = u.Surname
                                })
                                .FirstOrDefault()
                        })
                        .First()

                })
                .ToListAsync();

            return requests;
        }

        public async Task<IEnumerable<RequestDTO>?> GetRequestsBy_UserId(int userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            var requests = await context.Requests
                .Where(r => r.FromId == user.Id)
                .Select(r => new RequestDTO()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    CompleteDate = r.CompleteDate,
                    RequestStatusId = r.RequestStatusId,
                    RequestTypeId = r.RequestTypeId,
                    FromUser = context.Users
                        .Where(u => u.Id == r.FromId)
                        .Select(u => new UserDTO()
                        {
                            Id = u.Id,
                            Birthday = u.Birthday,
                            Name = u.Name,
                            Patronymic = u.Patronymic,
                            PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                            Surname = u.Surname
                        })
                        .FirstOrDefault(),
                    Images = context.RequestFiles
                        .Where(rf => rf.RequestId == r.Id)
                        .Select(rf => rf.FilePath)
                        .ToList(),
                    Cabinet = context.Cabinets
                        .Where(c => c.Id == r.CabId)
                        .Select(c => new CabinetDTO()
                        {
                            Id = c.Id,
                            Floor = c.Floor,
                            Height = c.Height,
                            Length = c.Length,
                            Num = c.Num,
                            PlanNum = c.PlanNum,
                            Width = c.Width,
                            ResponsiblePerson = context.Users
                                .Where(u => u.Id == r.FromId)
                                .Select(u => new UserDTO()
                                {
                                    Id = u.Id,
                                    Birthday = u.Birthday,
                                    Name = u.Name,
                                    Patronymic = u.Patronymic,
                                    PermissionName = context.Permissions.Where(p => p.Id == u.PermissionId).Select(p => p.Name).First(),
                                    Surname = u.Surname
                                })
                                .FirstOrDefault()
                        })
                        .First()

                })
                .ToListAsync();

            return requests;
        }
    }
}
