namespace BluNoro.Core.Common.DataObjects
{
    public class ConnectionStatus(IpPort adress, DateTime timeOfConnection)
    {
        public ConnectionStatus(string ipPort, DateTime timeOfConnection) : this(new IpPort(ipPort), timeOfConnection)
        {

        }

        public ConnectionStatus() : this("127.0.0.1:0", DateTime.Now)
        {
            
        }

        public IpPort Adress { get; set; } = adress;
        public DateTime TimeOfConnection { get; set; } = timeOfConnection;
        public bool IsConnected { get; set; } = false;

        public TimeSpan TimeOnServer()
        {
            return DateTime.Now - TimeOfConnection;
        }

        public string TimeOnServerFormatted()
        {
            return TimeOnServer().ToString(@"hh\:mm\:ss");
        }


    }
}
