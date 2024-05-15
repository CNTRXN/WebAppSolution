using System.Collections;
using WebAPI.DataContext.DTO;
using WebAPI.Models;

namespace WebAPI.Services.FileService
{
    public interface IFileService
    {
        #region Кабинеты
        Task<IEnumerable<string>?> GetCabinetFiles(int cabinetId);
        Task<IEnumerable<string>?> GetCabinetImages(int cabinetId);
        Task<string> GetCabinetFile(int cabinetId, int fileId);
        Task<string> GetCabinetImage(int cabinetId, int imageId);
        Task<bool> DeleteCabinetFile_ByFileId(int cabinetId, int fileId);
        Task<bool> DeleteCabinetImage_ByImageId(int cabinetId, int imageId);
        Task<bool> AddNewCabinetFile(FileUploadModel file, int cabinetId);
        Task<bool> AddNewCabinetImage(FileUploadModel image, int cabinetId);
        Task<bool> AddNewCabinetFiles(IEnumerable<FileUploadModel> file, int cabinetId);
        Task<bool> AddNewCabinetImages(IEnumerable<FileUploadModel> image, int cabinetId);
        #endregion

        #region Заявки
        Task<IEnumerable<string>> GetRequestFiles(int requestId);
        Task<IEnumerable<string>> GetRequestImages(int requestId);
        Task<string> GetRequestFile(int requestId, int fileId);
        Task<string> GetRequestImage(int requestId, int imageId);
        Task<bool> DeleteCabinetFile_ByFileName(int cabinetId, string name);
        Task<bool> DeleteCabinetImage_ByImageName(int cabinetId, string name); 
        Task<bool> AddNewRequestFile(FileUploadModel file, int requestId);
        Task<bool> AddNewRequestImage(FileUploadModel image, int requestId);
        Task<bool> AddNewRequestFiles(IEnumerable<FileUploadModel> file, int requestId);
        Task<bool> AddNewRequestImages(IEnumerable<FileUploadModel> image, int requestId);
        #endregion
    }
}