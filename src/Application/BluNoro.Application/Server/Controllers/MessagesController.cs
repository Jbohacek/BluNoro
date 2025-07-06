using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.MessageTypes.SendMessage.Confirmation;
using BluNoro.Core.Common.MessageTypes.SendMessage;
using BluNoro.Core.Contracts.Enums;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Server.Infrastructure;

namespace BluNoro.Core.Server.Controllers
{
    public class MessagesController(MessageServerManager manager) : BaseServerController(manager)
    {
        public void RequestToSend(ServerRequestSendMessage message)
        {

            //Check if chat exists
            var chatFromDatabase =
                _manager.Database.Chats.Include(x => x.Users).FirstOrDefault(x => x.Id == message.ParentChat.Id);
            if (chatFromDatabase == null)
            {
                _manager.Logger.Add(new Log("Chat not found", message.ParentChat.Id.ToString(), LogLevels.Level.ClientError));
                return;
            }

            //Send success Send
            var returnSuccess = new ClientSuccessSendMessage
            {
                UserConnection = message.UserConnection
            };
            _manager.Send(returnSuccess);

            message.Message.ArrivedTime = DateTime.Now;

            ClientBroadcastMessage responce = new ClientBroadcastMessage(message.ParentChat.Id, message.Message)
            {
                UserConnection = message.UserConnection
            };

            _manager.SendBroadCastChat(responce, message.ParentChat);

            _manager.Database.Messages.Add(message.Message);
            _manager.Database.Save();
        }

    }

}
