using WebAPI.DataContext.DTO;
using WebAPI.DataContext.Models;

namespace WebAPI.Services.EquipmentService
{
    public interface IEquipmentService
    {
        Task<IEnumerable<Equipment>> GetEquipments();
        Task<Equipment?> GetEquipment(int equipmentId);
        Task<int> GetEuipmentCountByType(string typeName);
        Task<Equipment?> AddNewEquipment(NewEquipmentDTO newEquipments);
        Task<int> AddNewEquipments(IEnumerable<NewEquipmentDTO> newEquipments);
        Task<bool> DeleteEquipment(int equipmentId);
        Task<int> DeleteEquipments(IEnumerable<EquipmentDTO> deletedEquipments);

        Task<IEnumerable<EquipmentType>> GetEquipmentTypes();
        Task<EquipmentType?> GetEquipmentType(int equipmentTypeId);
        Task<bool> AddNewEquipmentType(string typeName);
    }
}
