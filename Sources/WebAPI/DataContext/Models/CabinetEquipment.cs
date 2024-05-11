﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataContext.Models
{
    [Table("Оборудование в кабинетах")]
    public class CabinetEquipment
    {
        [Key]
        public int Id { get; set; }
        public int CabinetId { get; set; }
        public int EquipmentId { get; set; }
        //public int Count { get; set; }

        [ForeignKey("CabinetId")]
        public Cabinet Cabinet { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; }
    }
}
