namespace WebApp.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public DateTime Birthday { get; set; }
        public string PermissionName { get; set; }
    }
}
