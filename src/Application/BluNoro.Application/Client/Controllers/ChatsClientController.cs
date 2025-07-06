using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.MessageTypes.GetChatMessages;
using BluNoro.Core.Common.MessageTypes.GetChats;

namespace BluNoro.Core.Client.Controllers
{
    public class ChatsClientController(MessageClientManager manager) : BaseClientController(manager)
    {
        public void GetChatMessages(Chat chat)
        {
            ServerRequestToGetChatMessages request = new ServerRequestToGetChatMessages()
            {
                UserConnection = _manager.UserConnection,
                Chat = chat
            };
            _manager.Send(request);
        }

        public void ReloadChats()
        {
            ServerRequestChats request = new()
            {
                UserConnection = _manager.UserConnection, 
                UserId = _manager.UserConnection.User.Id
            };
            _manager.Send(request);

            _manager.Client.Events.Register<ClientReturnChats>((msg) =>
            {
                _manager.Client.UserConnection.User.Chats = msg.Chats;
            });

        }

    }
}
