namespace WebApp.Settings
{
    public class AppStatics
    {
        public static string ApiUrl { get; } = "http://localhost:5215/";

        public static HttpClient ApiClient { get; } = new()
        {
            BaseAddress = new Uri(ApiUrl)
        };
    }
}
