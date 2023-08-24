using System.Collections.Generic;
using System.Linq;

namespace ChatService.ConnectionMapping
{
    public class BaseConnectionMapping<T>
    {


        private readonly Dictionary<T,string> _connections =
            new Dictionary<T, string>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            var hasValue = _connections.ContainsKey(key);

            if(!hasValue)
            {
                _connections[key] = connectionId;

            }
        }

        public string? GetConnections(T key)
        {
            var hasValue = _connections.ContainsKey(key);

            if (hasValue)
            {
                string? value = _connections[key];

                return value;
            }
            return null;
        }

        public void Remove(T key)
        {
            var hasValue = _connections.ContainsKey(key);

            if (!hasValue)
            {
                return;
            }

            _connections.Remove(key);
        }
    }
}
