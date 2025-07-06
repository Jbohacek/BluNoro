using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.Common.DataObjects
{
    public class UserConnection
    {

        public UserConnection()
        {
            User = new AnonymousUser();
            ConnectionStatus = new AnonymousConnection();
        }

        public UserConnection(User user, ConnectionStatus status)
        {
            User = user;
            ConnectionStatus = status;
        }

        public UserConnection(ConnectionStatus status)
        {
            User = new AnonymousUser();
            ConnectionStatus = status;
        }


        public User User { get; set; }
        public ConnectionStatus ConnectionStatus { get; set; }

        public string IpPort => ConnectionStatus.Adress.ToString()!;

        public bool AmIAnonymousUser()
        {
            return User is AnonymousUser;
        }

    }
}
