using System.ComponentModel.DataAnnotations;

namespace ModelLib.DTO
{
    public class NewUserDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public int PostId { get; set; } = 1;
    }
}
