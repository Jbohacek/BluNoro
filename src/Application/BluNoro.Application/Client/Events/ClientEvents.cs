using BluNoro.Core.Common.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluNoro.Core.Client.Events
{
    public class ClientEvents
    {
        private readonly Dictionary<Type, List<Delegate>?> _handlers = new();
        public SynchronizationContext? SyncContext { get; set; }


        public void Register<T>(Action<T> handler) where T : MessageBaseClient
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<Delegate>();

            _handlers[type]!.Add(handler);
        }

        public void Dispatch<T>(T message) where T : MessageBaseClient
        {
            var type = message.GetType();

            if (!_handlers.TryGetValue(type, out var delegates)) return;

            foreach (var del in delegates!)
            {
                if (SyncContext != null)
                {
                    SyncContext.Post(x => del.DynamicInvoke(message),null);
                }
                else
                {
                    del.DynamicInvoke(message);
                }

            }
        }
    }
}
