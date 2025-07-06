using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.Attributes;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Infrastructure;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Server.Infrastructure;

namespace BluNoro.Core.Common.MessageTypes.Authenticate
{
    [AllowAnonymous]
    public class ServerVerification : MessageBaseServer
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            serverManager.AuthController.VerifyUser(this);
        }
    }
}
