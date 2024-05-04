using WebApp.Attributes;
using WebApp.Settings.Converters.Table;

namespace WebApp.Models.EquipData
{
    [AlternativeName("Тестовая таблица")]
    public class TestData : IMetaData
    {
        [AlternativeName("ИД")]
        public int Id { get; set; }
        [AlternativeName("Номер запроса")]
        public int Request { get; set; }
        [AlternativeName("От кого")]
        public dynamic From { get; set; }
    }
}
