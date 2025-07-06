using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Client.Infrastructure;
using BluNoro.Core.Common.MessageTypes.Authenticate;

namespace BluNoro.Core.Client.Controllers
{
    public class AuthClientController(MessageClientManager manager) : BaseClientController(manager)
    {
        public void SendAuthetication(string username, string password)
        {
            var request = new ServerVerification
            {
                UserName = username,
                Password = password,
                UserConnection = _manager.UserConnection
            };
            _manager.Send(request);
        }
    }
}
