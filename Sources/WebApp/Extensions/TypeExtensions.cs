namespace WebApp.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNullableType(this Type type)
        {
            if (type == null)
                return false;

            return Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsNullable<T>(T obj)
        {
            if (obj == null) return true; // obvious
            Type type = typeof(T);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }
    }
}
