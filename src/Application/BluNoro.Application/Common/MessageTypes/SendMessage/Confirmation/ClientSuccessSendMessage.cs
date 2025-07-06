using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.Common.MessageTypes.SendMessage.Confirmation
{
    public class ClientSuccessSendMessage : MessageBaseClient
    {
        public Message Message { get; set; }

        public ClientSuccessSendMessage(Message message)
        {
            Message = message;
        }

        public ClientSuccessSendMessage()
        {
            
        }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            
        }
    }
}
