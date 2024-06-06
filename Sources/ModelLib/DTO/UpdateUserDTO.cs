using ModelLib.Convert.Table;
using ModelLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLib.Convert.Attributes;

namespace ModelLib.DTO
{
    public class UpdateUserDTO : IMetaData
    {
        [Required, AlternativeName("Имя")]
        public string Name { get; set; }
        [Required, AlternativeName("Фамилия")]
        public string Surname { get; set; }
        [AlternativeName("Отчество")]
        public string? Patronymic { get; set; }
        [Required, DateValue, AlternativeName("День рождения")]
        public DateTime Birthday { get; set; }
        [Required, AlternativeName("Логин")]
        public string Login { get; set; }
        [Required, AlternativeName("Пароль")]
        public string Password { get; set; }
        [Required, SelectedValue(true), AlternativeName("Права доступа"), ShowPermission("Master, Admin")]
        public Permission Permission { get; set; }
    }
}