using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands
{
    internal class clearCommand(Server server) : Command(server)
    {
        public override string Name => "clear";
        public override string Description => "it will clear the console";
        public override string Format => "";

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;
            Console.Clear();
        }
    }
}
