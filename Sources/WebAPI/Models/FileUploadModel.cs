namespace WebAPI.Models
{
    public class FileUploadModel
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Descrption { get; set; }
        public int ImageAuthorId { get; set; }
    }
}
