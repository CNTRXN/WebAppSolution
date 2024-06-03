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
        public static string s_apiUrl { get; } = "http://localhost:5215/";
        public string ApiUrl => s_apiUrl;
        public string ApiCabinetRoute { get; } = "api/Cabinet/";
        public string ApiEquipmentRoute { get; } = "api/Equipment/";
        public string ApiEquipmentTypeRoute { get; } = "api/EquipmentType/";
        public string ApiPermissionRoute { get; } = "api/Permission/";
        public string ApiRequestRoute { get; } = "api/Request/";
        public string ApiRequestStatusRoute { get; } = "api/RequestStatus/";
        public string ApiRequestTypeRoute { get; } = "api/RequestType/";
        public string ApiUserRoute { get; } = "api/User/";

        public HttpClient Client { get; } = new()
        {
            BaseAddress = new Uri(s_apiUrl)
        };

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
