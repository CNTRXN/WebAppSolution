using ModelLib.Model;

namespace ModelLib.DTO
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InventoryNumber { get; set; }
        public EquipmentType EquipmentType { get; set; }
    }
}
