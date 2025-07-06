using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.Common.MessageTypes.GetChatMessages
{
    public class ClientMultipleStringMessages : MessageBaseClient
    {
        public List<Message> Content { get; set; } = new List<Message>();
        public Guid idChat { get; set; }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            if(clientManager.Client.UserConnection.AmIAnonymousUser())
                return;

            Chat? clientChat = clientManager.Client.UserConnection.User.Chats.FirstOrDefault(x => x.Id == idChat);
            
            if(clientChat == null)
                return;

            clientChat.Messages.Clear();
            clientChat.Messages = Content;

            base.MessangeHandler(clientManager);
        }
    }
}
