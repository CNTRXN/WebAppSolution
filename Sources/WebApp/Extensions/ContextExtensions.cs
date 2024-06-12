namespace WebApp.Extensions
{
    public static class ContextExtensions
    {
        private readonly static List<string> idNames = 
        [
            "cabid",
            "userid"
        ];

        public static void ClearAllIdentityCookie(this HttpContext Context)
        {
            if (Context != null)
                foreach (var name in idNames)
                {
                    if (Context.Request.Cookies.Any(c => c.Key == name))
                    {
                        Context.Response.Cookies.Delete(name);
                    }
                }
        }

        public static void DeleteCookieByName(this HttpContext Context, string name)
        {
            if (Context != null)
            {
                if (Context.Request.Cookies.Any(c => c.Key == name))
                {
                    Context.Response.Cookies.Delete(name);
                }
            }
        }

        public static object? GetValueFromCookie(this HttpContext Context, params string[] cookieNames)
        {
            object? id = null;

            foreach (var cookieName in cookieNames)
            {
                if (Context.Request.Cookies.TryGetValue(cookieName, out string? value))
                    id = value;
            }

            return id;
        }
    }
}
