namespace WebApp.Models.DTO
{
    public class UserRegDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
