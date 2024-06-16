using System.Net;

namespace WebApp.Settings
{
    public enum ApiRequestType
    {
        Cabinet,
        Equipment,
        EquipmentType,
        Permission,
        Request,
        RequestStatus,
        RequestType,
        User
    }
    public class ApiSettings
    {
        //public static string s_apiUrl { get; } = "http://localhost:5215/";
        //public static string s_apiUrl { get; } = "https://localhost:5002/";
        public static string s_apiUrl { get; } = "https://api.infotech-college.ru";
        public string ApiUrl => s_apiUrl;
        public string ApiCabinetRoute { get; } = "api/Cabinet/";
        public string ApiEquipmentRoute { get; } = "api/Equipment/";
        public string ApiEquipmentTypeRoute { get; } = "api/EquipmentType/";
        public string ApiPermissionRoute { get; } = "api/Permission/";
        public string ApiRequestRoute { get; } = "api/Request/";
        public string ApiRequestStatusRoute { get; } = "api/RequestStatus/";
        public string ApiRequestTypeRoute { get; } = "api/RequestType/";
        public string ApiUserRoute { get; } = "api/User/";
   
        private readonly static CookieContainer cookiesContainer = new();
        private readonly static HttpClientHandler clientHandler = new()
        {
            CookieContainer = cookiesContainer
        };
        private static Uri _baseAddress { get; } = new Uri(s_apiUrl);
        public HttpClient Client { get; } = new(clientHandler)
        {
            BaseAddress = _baseAddress
        };

        public void SetCookies(Dictionary<string, string> cookies) 
        {
            foreach (var item in cookies)
            {
                cookiesContainer.Add(_baseAddress, new Cookie() 
                {
                    Name = item.Key,
                    Value = item.Value,
                });
            }
        }

        public void SetHeaders(Dictionary<string, string> headers) 
        {
            Client.DefaultRequestHeaders.Clear();

            foreach (var item in headers) 
            {
                Client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
        }

        public void ClearHeaders() 
        {
            Client.DefaultRequestHeaders.Clear();
        }

        public string ApiRequestUrl(ApiRequestType apiRequestType, string route)
        {
            string requestRoute = apiRequestType switch
            {
                ApiRequestType.Cabinet => ApiCabinetRoute,
                ApiRequestType.Equipment => ApiEquipmentRoute,
                ApiRequestType.EquipmentType => ApiEquipmentTypeRoute,
                ApiRequestType.Permission => ApiPermissionRoute,
                ApiRequestType.Request => ApiRequestRoute,
                ApiRequestType.RequestStatus => ApiRequestStatusRoute,
                ApiRequestType.RequestType => ApiEquipmentTypeRoute,
                ApiRequestType.User => ApiUserRoute,
                _ => string.Empty
            };
            requestRoute += route;

            return requestRoute;
        }
    }
}
