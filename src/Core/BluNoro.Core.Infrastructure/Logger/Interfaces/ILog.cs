using BluNoro.Core.Contracts.Enums;

namespace BluNoro.Core.Infrastructure.Logger.Interfaces
{
    public interface ILog
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public LogLevels.Level Level { get; set; }

        public string GetLog();

    }
}