namespace WebAPI.Services
{
    public interface IFileService
    {
        Task<IEnumerable<string>> GetImages(int cabId);
    }
}
