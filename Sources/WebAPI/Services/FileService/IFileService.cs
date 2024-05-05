using WebAPI.DataContext.DTO;
using WebAPI.Models;

namespace WebAPI.Services.FileService
{
    public interface IFileService
    {
        #region Кабинеты
        Task<IEnumerable<FileModelDTO>> GetCabinetFiles(int cabinetId);
        Task<IEnumerable<FileModelDTO>> GetCabinetImages(int cabinetId);
        Task<FileModelDTO> GetCabinetFile(int cabinetId, int fileId);
        Task<FileModelDTO> GetCabinetImage(int cabinetId, int imageId);
        Task<bool> DeleteCabinetFile_ByFileId(int cabinetId, int fileId);
        Task<bool> DeleteCabinetImage_ByImageId(int cabinetId, int imageId);
        Task<bool> AddNewCabinetFile(FileUploadModel file, int cabinetId);
        Task<bool> AddNewCabinetImage(FileUploadModel image, int cabinetId);
        Task<bool> AddNewCabinetFiles(List<FileUploadModel> file, int cabinetId);
        Task<bool> AddNewCabinetImages(List<FileUploadModel> image, int cabinetId);
        #endregion

        #region Заявки
        Task<IEnumerable<FileModelDTO>> GetRequestFiles(int requestId);
        Task<IEnumerable<FileModelDTO>> GetRequestImages(int requestId);
        Task<FileModelDTO> GetRequestFile(int requestId, int fileId);
        Task<FileModelDTO> GetRequestImage(int requestId, int imageId);
        Task<bool> DeleteCabinetFile_ByFileName(int cabinetId, string name);
        Task<bool> DeleteCabinetImage_ByImageName(int cabinetId, string name); 
        Task<bool> AddNewRequestFile(FileUploadModel file, int requestId);
        Task<bool> AddNewRequestImage(FileUploadModel image, int requestId);
        Task<bool> AddNewRequestFiles(List<FileUploadModel> file, int requestId);
        Task<bool> AddNewRequestImages(List<FileUploadModel> image, int requestId);
        #endregion
    }
}