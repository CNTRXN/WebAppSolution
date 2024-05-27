using ModelLib.Model;
using ModelLib.Convert.Table;
using ModelLib.Convert.Attributes;

namespace ModelLib.DTO
{
    [AlternativeName("Пользователи")]
    public class UserDTO : IMetaData
    {
        [InclusionInHeader(HeaderInclusion.NotInclude)]
        public int Id { get; set; }
        [AlternativeName("Имя")]
        public string Name { get; set; }
        [AlternativeName("Фамилия")]
        public string Surname { get; set; }
        [AlternativeName("Отчество")]
        public string? Patronymic { get; set; }
        [AlternativeName("День рождения")]
        public DateTime Birthday { get; set; }
        [AlternativeName("Права доступа"), SelectedValue(true)]
        public Permission Permission { get; set; }
    }
}
