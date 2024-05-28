using ModelLib.Model;
using ModelLib.Convert.Table;
using ModelLib.Convert.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ModelLib.DTO
{
    [AlternativeName("Пользователи")]
    public class UserDTO : IMetaData
    {
        [InclusionInHeader(HeaderInclusion.NotInclude), Required]
        public int Id { get; set; }
        [AlternativeName("Имя"), Required]
        public string Name { get; set; }
        [AlternativeName("Фамилия"), Required]
        public string Surname { get; set; }
        [AlternativeName("Отчество")]
        public string? Patronymic { get; set; }
        [AlternativeName("День рождения"), Required]
        public DateTime Birthday { get; set; }
        [AlternativeName("Права доступа"), SelectedValue(true), Required]
        public Permission Permission { get; set; }
    }
}
