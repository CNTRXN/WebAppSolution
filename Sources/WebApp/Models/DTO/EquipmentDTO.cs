using WebApp.Attributes;

namespace WebApp.Models.DTO
{
    [AlternativeName("Оборудование")]
    public class EquipmentDTO : IMetaData
    {
        [AlternativeName("Имя")]
        public string Name { get; set; }
        [AlternativeName("Описание")]
        public string Description { get; set; }
        [AlternativeName("Тип")]
        public int TypeId { get; set; }
        [AlternativeName("Количество")]
        public int Count { get; set; }
    }
}
