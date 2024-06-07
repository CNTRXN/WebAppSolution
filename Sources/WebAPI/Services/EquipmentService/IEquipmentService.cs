using ModelLib.DTO;
using ModelLib.Model;

namespace WebAPI.Services.EquipmentService
{
    public interface IEquipmentService
    {
        Task<IEnumerable<EquipmentDTO>> GetEquipments();
        Task<EquipmentDTO?> GetEquipment(int equipmentId);
        Task<List<EquipmentDTO>?> GetEquipmentsById(IEnumerable<int> equipmentIds);
        Task<int> GetEuipmentCountByType(string typeName);
        Task<Equipment?> AddNewEquipment(NewEquipmentDTO newEquipments);
        Task<int> AddNewEquipments(IEnumerable<NewEquipmentDTO> newEquipments);
        Task<bool> DeleteEquipment(int equipmentId);
        Task<int> DeleteEquipments(IEnumerable<EquipmentDTO> deletedEquipments);
        Task<bool> UpdateEquipment(int equipmentId, NewEquipmentDTO updateEquipment);

        Task<IEnumerable<EquipmentType>> GetEquipmentTypes();
        Task<EquipmentType?> GetEquipmentType(int equipmentTypeId);
        Task<bool> AddNewEquipmentType(string typeName);
    }
}
