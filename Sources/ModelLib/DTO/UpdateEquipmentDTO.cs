using ModelLib.Convert.Attributes;
using ModelLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.DTO
{
    public class UpdateEquipmentDTO
    {
        [AlternativeName("Имя"), Required]
        public string Name { get; set; }

        [AlternativeName("Описание"), Required]
        public string Description { get; set; }

        [AlternativeName("Инвентарный номер"), Required]
        public string InventoryNumber { get; set; }

        [AlternativeName("Тип"), SelectedValue(true), Required, ShowPermission("Master, Admin")]
        public EquipmentType EquipmentType { get; set; }
    }
}
