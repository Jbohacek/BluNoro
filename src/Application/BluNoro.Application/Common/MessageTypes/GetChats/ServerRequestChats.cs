using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Server.Infrastructure;

namespace BluNoro.Core.Common.MessageTypes.GetChats
{
    public class ServerRequestChats : MessageBaseServer
    {
        public required Guid UserId { get; set; }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            serverManager.ChatController.Handle(this);
        }
    }
}
