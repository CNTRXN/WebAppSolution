using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.DataContext.Models
{
    [Table("Типы оборудования")]
    public class EquipmentType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
