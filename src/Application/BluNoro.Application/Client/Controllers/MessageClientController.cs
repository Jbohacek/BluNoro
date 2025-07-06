using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.MessageTypes.SendMessage;

namespace BluNoro.Core.Client.Controllers
{
    public class MessageClientController(MessageClientManager manager) : BaseClientController(manager)
    {
        public void SendMessage(string unformatedMessage, Chat chat)
        {
            if(_manager.UserConnection.AmIAnonymousUser())
                throw new Exception("No user");

            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                ParentChat = chat,
                Sender = _manager.UserConnection.User,
                UnformatedMessage = unformatedMessage
            };
            ServerRequestSendMessage messageRequest = new ServerRequestSendMessage()
            {
                UserConnection = _manager.UserConnection,
                Message = message,
                ParentChat = chat
            };
            _manager.Send(messageRequest);
        }
    }
}
