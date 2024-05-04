namespace WebAPI.Models
{
    public class FileUploadModel
    {
        public IFormFile File { get; set; }
        public int FileAuthorId { get; set; }
    }
}
