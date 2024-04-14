using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetEquipments();
        Task<Equipment?> GetEquipment(int id);
        Task<Equipment?> AddNewEquipment(EquipmentDTO newEquipments);
        Task<bool> AddNewEquipmentType(string typeName);
        Task<int?> AddNewEquipments(IEnumerable<EquipmentDTO> newEquipments);
        Task<bool> DeleteEquipment(int id);
        Task<int?> DeleteEquipments(IEnumerable<EquipmentDTO> deletedEquipments);
    }
}
