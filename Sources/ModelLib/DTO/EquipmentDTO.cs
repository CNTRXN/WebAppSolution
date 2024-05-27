using ModelLib.Convert.Attributes;
using ModelLib.Convert.Table;
using ModelLib.Model;

namespace ModelLib.DTO
{
    [AlternativeName("Оборудование")]
    public class EquipmentDTO : IMetaData
    {
        [InclusionInHeader(HeaderInclusion.NotInclude)]
        public int Id { get; set; }

        [AlternativeName("Имя")]
        public string Name { get; set; }

        [AlternativeName("Описание")]
        public string Description { get; set; }

        [AlternativeName("Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [AlternativeName("Тип"), SelectedValue(true)]
        public EquipmentType EquipmentType { get; set; }
    }
}
