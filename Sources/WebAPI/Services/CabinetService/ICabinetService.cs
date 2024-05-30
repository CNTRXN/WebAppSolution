using ModelLib.Model;
using ModelLib.DTO;

namespace WebAPI.Services.CabinetService
{
    public interface ICabinetService
    {
        Task<IEnumerable<CabinetDTO>> GetCabinets();
        Task<CabinetDTO> GetCabinet(int id);
        Task<IEnumerable<CabinetDTO>?> GetCabinetByUser(int userId);
        Task<Cabinet?> AddCabinet(NewCabinetDTO newCabinet);
        Task<IEnumerable<EquipmentDTO>> GetCabinetEquipments(int cabId);
        Task<int> AddEquipmentsToCabinet(int cabId, IEnumerable<int> equipmentsIds);
        Task<bool> DeleteCabinet(int id);
        Task<bool> UpdateCabinet(int id, NewCabinetDTO updateCabinet);
    }
}
