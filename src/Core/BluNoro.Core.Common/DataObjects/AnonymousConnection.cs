using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluNoro.Core.Common.DataObjects
{
    public class AnonymousConnection : ConnectionStatus
    {
        public AnonymousConnection() : base("127.0.0.0:0", DateTime.Now)
        {
       
        }

        public AnonymousConnection(IpPort adress, DateTime timeOfConnection) : base(adress, timeOfConnection)
        {

        }
    }
}
