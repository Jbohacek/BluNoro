namespace BluNoro.Core.Infrastructure.Logger.Interfaces
{
    public interface ILogLoader
    {
        public string Path { get; set; }
        public ICollection<ILog> Load();
    }
}