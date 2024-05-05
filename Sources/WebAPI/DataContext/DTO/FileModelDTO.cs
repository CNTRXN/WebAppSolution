namespace WebAPI.DataContext.DTO
{
    public class FileModelDTO
    {
        public string FilePath { get; set; }
        public UserDTO? FileAuthor { get; set; }
    }
}
