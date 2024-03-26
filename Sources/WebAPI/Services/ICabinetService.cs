using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services
{
    public interface ICabinetService
    {
        Task<IEnumerable<Cabinet>> GetCabinets();
        Task<Cabinet?> GetCabinet(int id);
        Task<Cabinet?> AddCabinet(CabinetDTO cabinet);
        Task<bool> DeleteCabinet(int id);
    }
}
