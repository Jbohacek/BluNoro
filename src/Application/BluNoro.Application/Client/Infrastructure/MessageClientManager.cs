using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Controllers;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.Attributes;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.MessageTypes.Authenticate;
using BluNoro.Core.Common.MessageTypes.GetChatMessages;
using BluNoro.Core.Common.MessageTypes.GetChats;
using BluNoro.Core.Common.MessageTypes.SendMessage;
using BluNoro.Core.Common.Serilization;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SuperSimpleTcp;

namespace BluNoro.Core.Client.Infrastructure
{
    public class MessageClientManager
    {
        private SimpleTcpClient SimpleTcp;
        public Client Client;
        public MessageSerializer Serializer = new MessageSerializer();
        private bool IsConnected => SimpleTcp.IsConnected;
        public UserConnection UserConnection => Client.UserConnection;

        public AuthClientController AuthClientController { get; private set; }
        public ChatsClientController ChatsClientController { get; private set; }
        public MessageClientController MessageClientController { get; private set; }

        public MessageClientManager(SimpleTcpClient simpleclient, Client client)
        {
            SimpleTcp = simpleclient;
            Client = client;

            AuthClientController = new AuthClientController(this);
            ChatsClientController = new ChatsClientController(this);
            MessageClientController = new MessageClientController(this);
        }

        //hotovo
        public void Send(MessageBaseServer message)
        {
            //Kontrola zda je připojený
            if (!IsConnected) throw new Exception("neni připojený");

            //Kontrola zda je nutné usera
            if (!message.GetType().GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
            {
                if (UserConnection.AmIAnonymousUser())
                    throw new Exception("Uživatel nemá usera a nemá pravomoce");
            }

            message.SendTime = DateTime.Now;
            string serilized = message.SerilizeMe();
            SimpleTcp.Send(serilized);
        }

        //
        public void ReciveMessage(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            //Todo: neni custom
            string data = Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);
            MessageBaseClient messageBaseClient = Serializer.DeserializeClientMessageFromString(data);
            messageBaseClient.RecievedTime = DateTime.Now;
            Debug.WriteLine((messageBaseClient.RecievedTime - messageBaseClient.SendTime).TotalMilliseconds + " ms Recived message");
            messageBaseClient.MessangeHandler(this);
            Debug.WriteLine($"[{e.IpPort}] {Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count)}");

        }
    }
}
