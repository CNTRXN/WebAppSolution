using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Оборудование в кабинетах")]
    public class CabinetEquipment
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int CabinetId { get; set; }
        [Required]
        public int EquipmentId { get; set; }
        //public int Count { get; set; }

        [ForeignKey("CabinetId")]
        public Cabinet Cabinet { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
