using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.Common.MessageTypes.Authenticate
{
    public class ClientSuccessVerification : MessageBaseClient
    {
        public required User AuthenticatedUser { get; set; }

        public override void MessangeHandler(MessageClientManager clientManager)
        {
            clientManager.Client.UserConnection.User = AuthenticatedUser;

            base.MessangeHandler(clientManager);
        }
    }
}
