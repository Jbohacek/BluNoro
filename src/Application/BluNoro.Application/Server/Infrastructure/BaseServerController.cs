using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Infrastructure;

namespace BluNoro.Core.Server.Infrastructure
{
    public abstract class BaseServerController(MessageServerManager manager)
    {
        protected readonly MessageServerManager _manager = manager;
        protected UnitOfWork Database => _manager.Database;


    }
}
