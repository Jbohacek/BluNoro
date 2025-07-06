using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperSimpleTcp;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Contracts.Enums;
using BluNoro.Core.Data.EF.Context;
using BluNoro.Core.Infrastructure;
using BluNoro.Core.Infrastructure.Logger.Interfaces;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Server.Infrastructure;


namespace BluNoro.Core.Server
{
    public class Server
    {
        public IpPort Adress { get; set; }

        private ILogger Logger { get; set; }

        private SimpleTcpServer tcpServer { get; set; }

        public List<User> ConnectedUsers { get; set; } = new List<User>();
        public List<ConnectionStatus> AnonymousUsers { get; set; } = new List<ConnectionStatus>();

        public UnitOfWork Database { get; set; }
        public MessageServerManager MessageServerManager { get; set; }
        
        public DateTime? ServerStartDate { get; set; }

        private Server()
        {
        }

        private void SetEvents()
        {
            if (tcpServer == null) throw new Exception("tcpServer is null");
            tcpServer.Events.ClientConnected += OnUserConnection!;
            tcpServer.Events.ClientDisconnected += OnUserDisconect!;
            tcpServer.Events.DataReceived += OnDataReceived;



            Logger.LogAdded += OnLogAdded!;
        }

        private void OnDataReceived(object? sender, DataReceivedEventArgs e)
        {
            string content = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);

            MessageServerManager.RecieveMessage(content,e.IpPort);
        }

        public void Start()
        {
            Logger.Add(LogFactory.ServerStarted(Adress));
            tcpServer.Start();
            ServerStartDate = DateTime.Now;
        }
        //todo: přidat stop možnost :D

        


        private void OnUserConnection(object sender, ConnectionEventArgs e)
        {
            ConnectionStatus status = new ConnectionStatus(e.IpPort,DateTime.Now); 
            AnonymousUsers.Add(status);
            Logger.Add(LogFactory.AnonymousUserConnected(new IpPort(e.IpPort)));


            //User user = new User(e.IpPort, DateTime.Now);
            //Logger.Add(LogFactory.UserConnected(user));
            //ConnectedUsers.Add(user);

            //tcpServer.Send(user.ServerAdress.ToString(), "Hello to tcpServer :)");
        }

        private void OnUserDisconect(object sender, ConnectionEventArgs e)
        {
            User? user = ConnectedUsers.SingleOrDefault(x => x.Adress.ToString() == e.IpPort);
            if (user != null)
            {
                ConnectedUsers.Remove(user);
                user.LastLogIn = DateTime.Now;
                Database.Users.Update(user);
                Database.Save();
                Logger.Add(LogFactory.UserDisconnected(user));
                return;
            }

            ConnectionStatus? anonymous = AnonymousUsers.SingleOrDefault(x => x.Adress.ToString() == e.IpPort);
            if(anonymous != null)
            {
                AnonymousUsers.Remove(anonymous);
                Logger.Add(LogFactory.AnonymousUserDisconnected(anonymous.Adress));
                return;
            }

            Logger.Add(LogFactory.UserNotFound(e.IpPort));
        }

        public void OnLogAdded(object sender, LogEventHandler e)
        {
            Console.ForegroundColor = LogLevels.LevelExtensions.ToConsoleColor(e.Log.Level);
            Console.WriteLine(e.Log);
            Console.ResetColor();
        }

        private void OnServerClose(object sender, EventArgs e)
        {
            Logger.Add(LogFactory.ServerStopping());
        }

        public class ServerBuilder()
        {
            private readonly Server _server = new Server();
            private SimpleTcpServerSettings _settings = new SimpleTcpServerSettings();
            private readonly User _adminUser = new User();

            public ServerBuilder SetAdress(IpPort adress)
            {
                _server.Adress = adress;
                return this;
            }


            public ServerBuilder SetLogger(ILogger logger)
            {
                _server.Logger = logger;
                return this;
            }

            public ServerBuilder SetSettings(SimpleTcpServerSettings settings)
            {
                _settings = settings;
                return this;
            }

            public ServerBuilder SetDatabase(SqlLiteContext context)
            {
                _server.Database = new UnitOfWork(context);
                return this;
            }

            public ServerBuilder SetAdminUserPassword(string password)
            {
                _adminUser.UserName = "Admin";
                _adminUser.HashPassword = password;
                _adminUser.HashUserPassword();
                return this;
            }

            public ServerBuilder SetOnClosingEvent()
            {
                AppDomain.CurrentDomain.ProcessExit += _server.OnServerClose!;
                return this;
            }


            public Server Build()
            {
                if (_server.Adress == null) throw new Exception("Address not added");
                if (_server.Logger == null) throw new Exception("Logger not set");
                if (_server.Database == null) throw new Exception("Database not set");

                //Server inicilaization
                _server.tcpServer = new SimpleTcpServer(_server.Adress.ToString());
                _server.SetEvents();

                //Database initialization
                _server.Database.Logger = _server.Logger;

                if (!_server.Database.Users.Exists(x => x.UserName == _adminUser.UserName))
                {
                    _server.Database.Users.Add(_adminUser);
                    _server.Database.Save();
                }

                //Server ChatManager
                _server.MessageServerManager = new MessageServerManager(_server.Database, _server.Logger,_server.tcpServer,_server);
                
                

                return _server;
            }

        }

    }
}

