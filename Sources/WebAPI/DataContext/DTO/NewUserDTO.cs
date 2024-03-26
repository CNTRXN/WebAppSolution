namespace WebAPI.DataContext.DTO
{
    public class NewUserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int PostId { get; set; } = 1;
    }
}
