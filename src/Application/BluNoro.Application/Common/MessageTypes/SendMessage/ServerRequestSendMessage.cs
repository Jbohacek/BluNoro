using System;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.MessageTypes.SendMessage.Confirmation;
using BluNoro.Core.Contracts.Enums;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Server.Infrastructure;

namespace BluNoro.Core.Common.MessageTypes.SendMessage
{
    public class ServerRequestSendMessage : MessageBaseServer
    {
        public Message Message { get; set; }
        public Chat ParentChat { get; set; }

        public ServerRequestSendMessage(Message message)
        {
            Message = message;
            ParentChat = message.ParentChat;
        }

        public ServerRequestSendMessage()
        {
            
        }


        public override void MessangeHandler(MessageServerManager serverManager)
        {
            serverManager.MessagesController.RequestToSend(this);
        }

        public override Message Convert()
        {
            return new Message()
            {
                Id = Message.Id,
                ParentChat = Message.ParentChat,
                Sender = Message.Sender,
                UnformatedMessage = Message.UnformatedMessage
            };
        }
    }
}
