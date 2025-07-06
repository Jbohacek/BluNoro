using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Events;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using SuperSimpleTcp;
using DataReceivedEventArgs = SuperSimpleTcp.DataReceivedEventArgs;

namespace BluNoro.Core.Client
{
    public class Client
    {
        private readonly SimpleTcpClient _client;

        public SimpleTcpClientEvents TcpEvents => _client.Events;

        public UserConnection UserConnection;
        public bool HasUser => UserConnection.User != null;

        public bool IsConnected => _client.IsConnected;
        public IpPort ServerAdress { get; set; }
        public MessageClientManager Manager { get; set; }

        public ClientEvents Events { get; set; }

        public Client(string ipAdress, int port)
        {
            ServerAdress = new IpPort(ipAdress, port);
            _client = new SimpleTcpClient($"{ipAdress}:{port}");
            Manager = new MessageClientManager(_client, this);
            Events = new ClientEvents();

            UserConnection = new UserConnection();

            _client.Events.Connected += Connected!;
            _client.Events.Disconnected += Disconnected!;
            _client.Events.DataReceived += Manager.ReciveMessage!;
        }

        public void Connect()
        {
            if(IsConnected) return;
            _client.Connect();
        }

        public void Disconnect()
        {
            if(!IsConnected) return;
            _client.Disconnect();
        }

        static void Connected(object sender, ConnectionEventArgs e)
        {
            Debug.WriteLine($"*** Server {e.IpPort} connected");
        }

        static void Disconnected(object sender, ConnectionEventArgs e)
        {
            Debug.WriteLine($"*** Server {e.IpPort} disconnected");
        }


    }
}
