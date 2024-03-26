using WebApp.Models.DTO;

namespace WebApp.Settings
{
    public class AppStatics
    {
        public static UserDTO? User { get; set; }
        public static string ApiUrl { get; set; } = "http://localhost:5215/";
    }
}
