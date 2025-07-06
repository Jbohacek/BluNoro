using System.ComponentModel;

namespace BluNoro.Core.Infrastructure.Logger.Interfaces
{
    public interface ILogger
    { 
        ICollection<ILog> Logs { get; set; }
        public ILogSaver? LogSaver { get; set; }
        public ILogLoader? LogLoader { get; set; }

        public List<ILog> ToList();
        public BindingList<ILog> GetBindingList();
        

        public int GetCount();
        public void Add(ILog log);
        public void Remove(ILog log);
        public void Clear();

        public event EventHandler<LogEventHandler> LogAdded;
        void OnLogAdded(ILog log);

    }
}
