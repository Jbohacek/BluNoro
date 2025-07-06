namespace BluNoro.Core.Infrastructure.Logger.Interfaces
{
    public interface ILogSaver
    {
        public ILogger Logger { get; set; }
        public string Path { get; set; }
        public void Write(bool append = true);

    }
}