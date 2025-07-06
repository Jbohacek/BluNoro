using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands
{
    public class NotFoundCommand(Server server) : Command(server)
    {
        public override string Name => "NotFound";
        public override string Format => "";
        public override string Description => "This will be command for notFound";

        public override void InvokeCommand(string[] inputs)
        {
            Commander.SendErrorMessage("Command not found - try \"Help\" for more commands");
        }
    }
}
