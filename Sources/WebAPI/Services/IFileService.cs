namespace WebAPI.Services
{
    public interface IFileService
    {
        Task<IEnumerable<string>> GetFiles(int cabId);
    }
}
