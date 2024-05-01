using WebAPI.DataContext.Models;

namespace WebAPI.DataContext.DTO
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EquipmentType EquipmentType { get; set; }
        public int Count { get; set; }
    }
}
