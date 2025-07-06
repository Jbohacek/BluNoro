namespace BluNoro.Core.Common.DataObjects
{
    public class IpPort
    {
        public string Ip { get; set; } = null!;
        public int Port { get; set; }

        public IpPort(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        public IpPort(string ipPort)
        {
            string[] split = ipPort.Split(':');
            Ip = split[0];
            Port = int.Parse(split[1]);
        }

        public IpPort()
        {
            Ip = "127.0.0.1";
            Port = 0;
        }

        public override string ToString()
        {
            return Ip + ":" + Port;
        }

        public IpPort Clone()
        {
            return new IpPort(Ip, Port);
        }
    }
}
