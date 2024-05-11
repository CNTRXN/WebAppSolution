namespace WebAPI.DataContext.DTO
{
    public class NewEquipmentDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string InventoryNumber { get; set; }
        public int TypeId { get; set; }
        public int Count { get; set; }
    }
}
