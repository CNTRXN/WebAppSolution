using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Оборудование")]
    public class Equipment
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string InventoryNumber { get; set; }
        [Required]
        public int TypeId { get; set; }
        //public int Count { get; set; }

        [ForeignKey("TypeId")]
        public EquipmentType EquipmentType { get; set; }
    }
}
