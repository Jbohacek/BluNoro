using BluNoro.Core.Infrastructure.Logger.Interfaces;


namespace BluNoro.Core.Infrastructure.Logger
{
    public class LogEventHandler(ILog log) : EventArgs
    {
        public ILog Log { get; set; } = log;
        public DateTime Time = DateTime.Now;
    }
}
