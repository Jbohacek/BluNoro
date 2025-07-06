using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using SuperSimpleTcp;
using BluNoro.Core.Infrastructure;
using BluNoro.Core.Infrastructure.Logger.Interfaces;
using BluNoro.Core.Common.Serilization;
using BluNoro.Core.Server.Controllers;
using BluNoro.Core.Common.Abstracts;

namespace BluNoro.Core.Server.Infrastructure
{
    /// <summary>
    /// Slouží k posílání a uchově zpráv na serveru
    /// </summary>
    public class MessageServerManager
    {
        private SimpleTcpServer _tcpServer { get; set; }
        public Server Parent { get; private set; }
        public UnitOfWork Database { get; private set; }
        public ILogger Logger { get; private set; }
        private readonly MessageSerializer serializer = new MessageSerializer();

        public ChatController ChatController { get; private set; }
        public MessagesController MessagesController { get; private set; }
        public AuthController AuthController { get; private set; }

        public MessageServerManager(UnitOfWork database, ILogger logger, SimpleTcpServer tcpServer, Server parent)
        {
            Database = database;
            Logger = logger;
            _tcpServer = tcpServer;
            Parent = parent;

            ChatController = new ChatController(this);
            MessagesController = new MessagesController(this);
            AuthController = new AuthController(this);
        }

        public bool RecieveMessage(string messageString, string ipPort)
        {
            MessageBaseServer messageBaseServer = serializer.DeserializeServerMessageFromString(messageString);
            messageBaseServer.RecievedTime = DateTime.Now;
            messageBaseServer.UserConnection.ConnectionStatus = new ConnectionStatus(ipPort, DateTime.Now); //Todo: opravit
            messageBaseServer.MessangeHandler(this);
            return true;
        }

        public void Send(MessageBaseClient message, string overPort = "")
        {
            message.SendTime = DateTime.Now;
            string ipPort = string.IsNullOrEmpty(overPort) ? message.UserConnection.IpPort : overPort;
            string serilzed = message.SerilizeMe();

            
            _tcpServer.Send(ipPort,serilzed);
        }

        public void SendBroadCastChat(MessageBaseBroadcast message, Chat chat)
        {
            message.SendTime = DateTime.Now;

            var chatUsers = Database.Chats.Include(x => x.Users).First(x => x.Id == chat.Id).Users.ToList();

            var users = Parent.ConnectedUsers.Intersect(chatUsers).ToList();

            foreach (var user in Parent.ConnectedUsers.Intersect(chatUsers))
            {
                if (user.ServerStatus.IsConnected)
                {
                    Send(message,user.ServerStatus.Adress.ToString());
                }
            }

        }

    }
}
