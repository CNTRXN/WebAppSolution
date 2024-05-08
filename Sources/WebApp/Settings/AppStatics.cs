using WebApp.Models.DTO;

namespace WebApp.Settings
{
    public class AppStatics
    {
        //public static UserDTO? User { get; set; }
        public static string ApiUrl { get; } = "http://localhost:5215/";

        public static HttpClient ApiClient { get; } = new()
        {
            BaseAddress = new Uri(ApiUrl)
        };

        //public static string CabinetRoles { get; } = "User, Master";
    }
}
