using BluNoro.Core.Infrastructure;
using BluNoro.Core.Server.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluNoro.Core.Client.Infrastructure
{
    public abstract class BaseClientController(MessageClientManager manager)
    {
        protected readonly MessageClientManager _manager = manager;
    }
}
