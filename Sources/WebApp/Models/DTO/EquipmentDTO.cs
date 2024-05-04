using WebApp.Attributes;
using WebApp.Models.EquipData;
using WebApp.Settings.Converters.Table;

namespace WebApp.Models.DTO
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
        [AlternativeName("Тип")]
        public EquipmentType EquipmentType { get; set; }
        [AlternativeName("Количество")]
        public int Count { get; set; }
    }
}
