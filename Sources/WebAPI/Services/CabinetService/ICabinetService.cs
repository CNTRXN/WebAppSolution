using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.CabinetService
{
    public interface ICabinetService
    {
        Task<IEnumerable<CabinetDTO>> GetCabinets();
        Task<CabinetDTO> GetCabinet(int id);
        Task<Cabinet?> AddCabinet(NewCabinetDTO newCabinet);
        Task<IEnumerable<EquipmentDTO>> GetCabinetEquipments(int cabId);
        Task<int> AddEquipmentsToCabinet(int cabId, IEnumerable<int> equipmentsIds);
        Task<bool> DeleteCabinet(int id);
    }
}
