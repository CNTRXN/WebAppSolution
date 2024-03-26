using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    [Table("Оборудоввание")]
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int Count { get; set; }

        [ForeignKey("TypeId")]
        public EquipmentType EquipmentType { get; set; }
    }
}
