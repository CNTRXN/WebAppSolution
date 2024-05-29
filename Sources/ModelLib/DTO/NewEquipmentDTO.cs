using System.ComponentModel.DataAnnotations;

namespace ModelLib.DTO
{
    public class NewEquipmentDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string InventoryNumber { get; set; }
        [Required]
        public int TypeId { get; set; }
        /*[Required]
        public int Count { get; set; }*/
    }
}
