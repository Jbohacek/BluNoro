using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;
using BluNoro.Core.Server.Infrastructure;

namespace BluNoro.ServerConsole.Commands
{
    internal class StatusCommand(Server server) : Command(server)
    {
        public override string Name => "status";
        public override string Description => "this will show you status of the server";
        public override string Format => "";

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;
            ServerConsoleInterface consoleInterface = new ServerConsoleInterface(server);

            Console.WriteLine(consoleInterface.GetStatus());
            Console.WriteLine(consoleInterface.GetUsersList());
        }
    }
}
