using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Пользователи")]
    public class User
    {
        [Key]
        public int Id { get; set; }//Номер записи
        public string Name { get; set; }//Имя пользователя
        public string Surname { get; set; }//Фамилия пользователя
        public string? Patronymic { get; set; }//Отчество пользователя
        public DateTime Birthday { get; set; }//День рождения пользователя
        public string Login { get; set; }//Логин пользователя
        public string Password { get; set; }//Пароль
        public int PermissionId { get; set; }//Номер привилегии пользователя


        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
    }
}
