using BluNoro.Core.Contracts.Enums;
using BluNoro.Core.Infrastructure.Logger.Interfaces;

namespace BluNoro.Core.Infrastructure.Logger
{
    public class Log : ILog
    {
        public Log(string name,string? message = "", LogLevels.Level level = LogLevels.Level.Informal)
        {
            CreateLog(name,message, DateTime.Now,level );
        }
        public Log(string name, LogLevels.Level level = LogLevels.Level.Informal, string? message = "")
        {
            CreateLog(name, message, DateTime.Now, level);
        }

        private void CreateLog(string name ,string? message, DateTime time,LogLevels.Level level)
        {
            Name = name;
            Content = message;
            Date = time;
            Level = level;
            Id = Guid.NewGuid();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; }
        public LogLevels.Level Level { get; set; }

        public string GetLog()
        {
            return $"[{Date:yyyy-MM-dd HH:mm:ss}] [{Level.ToString()}] - {Name} ({Content})";
        }

        public override string ToString()
        {
            return GetLog();
        }
    }
}