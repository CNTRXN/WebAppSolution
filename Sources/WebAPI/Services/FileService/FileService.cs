
using Microsoft.EntityFrameworkCore;
using WebAPI.DataContext;
using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;
using WebAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Services.FileService
{
    public class FileService(DB_Context context) : IFileService
    {
        public async Task<bool> AddNewCabinetFile(FileUploadModel file, int cabinetId)
        {
            //существует ли запись кабинета
            var cabinet = await context.Cabinets
                .Where(c => c.Id == cabinetId)
                .FirstOrDefaultAsync();

            if (cabinet == null)
                return false;

            //существует ли уч.запись автора
            User? author = null;
            if (file.FileAuthorId is int authorId)
            {
                author = await context.Users
                    .Where(u => u.Id == authorId)
                    .FirstOrDefaultAsync();

                if (author == null)
                    return false;
            }

            string savePath = Path.Combine("Resources", "documents", "cabinets", cabinet.Id.ToString());

            //если нет пути сохранения, то он создаётся
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            //проверка есть ли такой файл в БД
            string fullPath = string.Empty;
            while (true)
            {
                var fileName = $"file{Guid.NewGuid():N}{Path.GetExtension(file.File.FileName)}";
                fullPath = Path.Combine(savePath, fileName);

                if (!context.CabinetFiles.Any(cp => cp.FilePath == fullPath))
                    break;
            }

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.File.CopyToAsync(stream);

            await context.CabinetFiles.AddAsync(new CabinetFiles()
            {
                CabinetId = cabinet.Id,
                FileAuthor = author?.Id,
                FilePath = fullPath
            });
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddNewCabinetFiles(List<FileUploadModel> files, int cabinetId)
        {
            //существует ли запись кабинета
            var cabinet = await context.Cabinets
                .Where(c => c.Id == cabinetId)
                .FirstOrDefaultAsync();

            if (cabinet == null)
                return false;

            //существует ли уч.запись автора
            List<User?> auhtors = [];
            foreach (var file in files) 
            {
                if (file.FileAuthorId is int authorId)
                {
                    var author = await context.Users
                        .Where(u => u.Id == authorId)
                        .FirstOrDefaultAsync();

                    if (author == null)
                    {
                        //return false;
                        auhtors.Add(null);
                    }

                    auhtors.Add(author);
                }
            }

            string savePath = Path.Combine("Resources", "documents", "cabinets", cabinet.Id.ToString());

            //если нет пути сохранения, то он создаётся
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            List<CabinetFiles> uploadedFiles = [];
            int i = 0;
            foreach (var file in files) 
            {
                string fullPath = string.Empty;
                while (true)
                {
                    var fileName = $"file{Guid.NewGuid():N}{Path.GetExtension(file.File.FileName)}";
                    fullPath = Path.Combine(savePath, fileName);

                    if (!context.CabinetFiles.Any(cp => cp.FilePath == fullPath))
                        break;
                }

                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.File.CopyToAsync(stream);

                uploadedFiles.Add(new CabinetFiles() 
                {
                    CabinetId = cabinet.Id,
                    FileAuthor = auhtors[i]?.Id,
                    FilePath = fullPath
                });
            }

            await context.CabinetFiles.AddRangeAsync(uploadedFiles);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddNewCabinetImage(FileUploadModel image, int cabinetId)
        {
            //существует ли запись кабинета
            var cabinet = await context.Cabinets
                .Where(c => c.Id == cabinetId)
                .FirstOrDefaultAsync();

            if (cabinet == null)
                return false;

            //существует ли уч.запись автора
            User? author = null;
            if (image.FileAuthorId is int authorId)
            {
                author = await context.Users
                    .Where(u => u.Id == authorId)
                    .FirstOrDefaultAsync();

                if (author == null)
                    return false;
            }

            string savePath = Path.Combine("Resources", "images", "cabinets", cabinet.Id.ToString());

            //если нет пути сохранения, то он создаётся
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            //проверка есть ли такой файл в БД
            string fullPath = string.Empty;
            while (true)
            {
                var fileName = $"img{Guid.NewGuid():N}{Path.GetExtension(image.File.FileName)}";
                fullPath = Path.Combine(savePath, fileName);

                if (!context.CabinetFiles.Any(cp => cp.FilePath == fullPath))
                    break;
            }

            using var stream = new FileStream(fullPath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            await context.CabinetFiles.AddAsync(new CabinetFiles()
            {
                CabinetId = cabinet.Id,
                FileAuthor = author?.Id,
                FilePath = fullPath
            });
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddNewCabinetImages(List<FileUploadModel> images, int cabinetId)
        {
            //существует ли запись кабинета
            var cabinet = await context.Cabinets
                .Where(c => c.Id == cabinetId)
                .FirstOrDefaultAsync();

            if (cabinet == null)
                return false;

            //существует ли уч.запись автора
            List<User?> auhtors = [];
            foreach (var image in images)
            {
                if (image.FileAuthorId is int authorId)
                {
                    var author = await context.Users
                        .Where(u => u.Id == authorId)
                        .FirstOrDefaultAsync();

                    if (author == null)
                    {
                        //return false;
                        auhtors.Add(null);
                    }

                    auhtors.Add(author);
                }
            }

            string savePath = Path.Combine("Resources", "images", "cabinets", cabinet.Id.ToString());

            //если нет пути сохранения, то он создаётся
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            List<CabinetFiles> uploadedFiles = [];
            int i = 0;
            foreach (var image in images)
            {
                string fullPath = string.Empty;
                while (true)
                {
                    var fileName = $"img{Guid.NewGuid():N}{Path.GetExtension(image.File.FileName)}";
                    fullPath = Path.Combine(savePath, fileName);

                    if (!context.CabinetFiles.Any(cp => cp.FilePath == fullPath))
                        break;
                }

                using var stream = new FileStream(fullPath, FileMode.Create);
                await image.File.CopyToAsync(stream);

                uploadedFiles.Add(new CabinetFiles()
                {
                    CabinetId = cabinet.Id,
                    FileAuthor = auhtors[i]?.Id,
                    FilePath = fullPath
                });
            }

            await context.CabinetFiles.AddRangeAsync(uploadedFiles);
            await context.SaveChangesAsync();

            return true;
        }

        public Task<bool> AddNewRequestFile(FileUploadModel file, int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddNewRequestFiles(List<FileUploadModel> file, int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddNewRequestImage(FileUploadModel image, int requestId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddNewRequestImages(List<FileUploadModel> images, int requestId)
        {
            //существует ли запись кабинета
            var request = await context.Requests
                .Where(c => c.Id == requestId)
                .FirstOrDefaultAsync();

            if (request == null)
                return false;

            //существует ли уч.запись автора
            /*List<User?> auhtors = [];
            foreach (var image in images)
            {
                if (image.FileAuthorId is int authorId)
                {
                    var author = await context.Users
                        .Where(u => u.Id == authorId)
                        .FirstOrDefaultAsync();

                    if (author == null)
                    {
                        //return false;
                        auhtors.Add(null);
                    }

                    auhtors.Add(author);
                }
            }*/

            string savePath = Path.Combine("Resources", "images", "requests", request.Id.ToString());

            //если нет пути сохранения, то он создаётся
            if (!Directory.Exists(savePath))
                Directory.CreateDirectory(savePath);

            List<RequestFile> uploadedFiles = [];
            //int i = 0;
            foreach (var image in images)
            {
                string fullPath = string.Empty;
                while (true)
                {
                    var fileName = $"img{Guid.NewGuid():N}{Path.GetExtension(image.File.FileName)}";
                    fullPath = Path.Combine(savePath, fileName);

                    if (!context.RequestFiles.Any(cp => cp.FilePath == fullPath))
                        break;
                }

                using var stream = new FileStream(fullPath, FileMode.Create);
                await image.File.CopyToAsync(stream);

                uploadedFiles.Add(new RequestFile()
                {
                    RequestId = request.Id,
                    FilePath = fullPath
                });
            }

            await context.RequestFiles.AddRangeAsync(uploadedFiles);
            await context.SaveChangesAsync();

            return true;
        }

        public Task<bool> DeleteCabinetFile_ByFileId(int cabinetId, int fileId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCabinetFile_ByFileName(int cabinetId, string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCabinetImage_ByImageId(int cabinetId, int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCabinetImage_ByImageName(int cabinetId, string name)
        {
            throw new NotImplementedException();
        }

        public Task<FileModelDTO> GetCabinetFile(int cabinetId, int fileId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileModelDTO>> GetCabinetFiles(int cabinetId)
        {
            throw new NotImplementedException();
        }

        public Task<FileModelDTO> GetCabinetImage(int cabinetId, int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileModelDTO>> GetCabinetImages(int cabinetId)
        {
            throw new NotImplementedException();
        }

        public Task<FileModelDTO> GetRequestFile(int requestId, int fileId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileModelDTO>> GetRequestFiles(int requestId)
        {
            throw new NotImplementedException();
        }

        public Task<FileModelDTO> GetRequestImage(int requestId, int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileModelDTO>> GetRequestImages(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
