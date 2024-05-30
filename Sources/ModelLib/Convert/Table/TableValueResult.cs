using System.Collections;
using System.Linq;

namespace ModelLib.Convert.Table
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

        public bool HasValue(string key)
        {
            return _tableResults.ContainsKey(key);
        }

        public Dictionary<string, object?> GetValues() 
        {
            return _tableResults;
        }

        public void SetValues(Dictionary<string, object?> newValues) 
        {
            _tableResults = newValues;
        }
    }

    /*public static class TableValueResultExtensions 
    {
        public static IEnumerable<T>? Select<T>(this TableValueResult list, Func<T, bool> func) 
        {
            var result = list.Items.Cast<T>().Where(func).ToList();

            if (result == null)
                return null;

            return result;
        }
    }*/
}
