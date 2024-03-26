using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataContext.Models
{
    [Table("Оборудование в кабинетах")]
    public class CabEquipment
    {
        [Key]
        public int Id { get; set; }
        public int CabId { get; set; }
        public int EquipId { get; set; }
        public int Count { get; set; }

        [ForeignKey("CabId")]
        public Cabinet Cabinet { get; set; }

        [ForeignKey("EquipId")]
        public Equipment Equipment { get; set; }
    }
}
