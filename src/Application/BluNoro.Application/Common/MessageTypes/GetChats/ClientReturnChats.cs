using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.Common.MessageTypes.GetChats
{
    public class ClientReturnChats : MessageBaseClient
    {
        public required List<Chat> Chats { get; set; }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            base.MessangeHandler(clientManager);
        }
    }
}
