using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Оборудование заявок")]
    public class RequestEquipment
    {
        [Key]
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int EquipmentId { get; set; }

        [ForeignKey("RequestId")]
        public Request Request { get; set; }
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
