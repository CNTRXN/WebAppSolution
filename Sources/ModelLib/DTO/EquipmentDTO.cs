using ModelLib.Convert.Attributes;
using ModelLib.Convert.Table;
using ModelLib.Model;
using System.ComponentModel.DataAnnotations;

namespace ModelLib.DTO
{
    [AlternativeName("Оборудование")]
    public class EquipmentDTO : IMetaData
    {
        [InclusionInHeader(HeaderInclusion.NotInclude), Required]
        public int Id { get; set; }

        [AlternativeName("Имя"), Required]
        public string Name { get; set; }

        [AlternativeName("Описание"), Required]
        public string Description { get; set; }

        [AlternativeName("Инвентарный номер"), Required]
        public string InventoryNumber { get; set; }

        [AlternativeName("Тип"), SelectedValue(true), Required]
        public EquipmentType EquipmentType { get; set; }
    }
}
