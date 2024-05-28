using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLib.Model
{
    [Table("Пользователи")]
    public class User
    {
        [Key, Required]
        public int Id { get; set; }//Номер записи
        [Required]
        public string Name { get; set; }//Имя пользователя
        [Required]
        public string Surname { get; set; }//Фамилия пользователя
        public string? Patronymic { get; set; }//Отчество пользователя
        [Required]
        public DateTime Birthday { get; set; }//День рождения пользователя
        [Required]
        public string Login { get; set; }//Логин пользователя
        [Required]
        public string Password { get; set; }//Пароль
        [Required]
        public int PermissionId { get; set; }//Номер привилегии пользователя


        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
    }
}
