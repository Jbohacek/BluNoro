using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands
{
    internal class ExitCommand(Server server) : Command(server)
    {
        public override string Name => "exit";

        public override string Description => "it will shut down the server";
        public override string Format => "";

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;
            Environment.Exit(0);
        }
    }
}
