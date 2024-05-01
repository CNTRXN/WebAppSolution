namespace WebApp.Settings
{
    public class TableValueResult
    {
        private Dictionary<string, object?> _tableResults = [];
        public int Count 
        { 
            get 
            {
                return _tableResults.Count;
            }
        }

        public object? this[string key] 
        {
            get 
            {
                if (_tableResults.TryGetValue(key, out object? value))
                {
                    return value;
                }

                return null;
            }

            set 
            {
                _tableResults[key] = value;
            }
        }
    }
}
