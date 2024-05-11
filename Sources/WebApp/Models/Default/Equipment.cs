using WebApp.Attributes;
using WebApp.Settings.Converters.Table;

namespace WebApp.Models.EquipData
{
    [AlternativeName("Оборудование")]
    public class Equipment : IMetaData
    {
        [AlternativeName("ID")]
        public int Id { get; set; }

        [AlternativeName("Имя")]
        public string Name { get; set; }

        [AlternativeName("Описание")]
        public string Description { get; set; }

        [AlternativeName("Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [AlternativeName("ID типа оборудования")]
        public int TypeId { get; set; }

        //[AlternativeName("Количество")]
        //public int Count { get; set; }
    }
}
