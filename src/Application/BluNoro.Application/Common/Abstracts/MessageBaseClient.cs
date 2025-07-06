using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Serilization;
using BluNoro.Core.Contracts.Abstracts;
namespace BluNoro.Core.Common.Abstracts
{
    public abstract class MessageBaseClient : MessageBase
    {
        public required UserConnection UserConnection { get; set; }

        public virtual void MessangeHandler(MessageClientManager clientManager)
        {
            clientManager.Client.Events.Dispatch(this);
        }


    }
}
