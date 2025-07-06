using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole
{
    public abstract class Command(Server server)
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string Format { get; }
        private Server Server { get; set; } = server;

        public abstract void InvokeCommand(string[] inputs);

        public bool CheckFormat(string[] inputs)
        {
            if (string.IsNullOrEmpty(Format))
                return true;

            int count = Format.Split(' ').Length;

            if (inputs.Length != count + 1)
            {
                Commander.SendErrorMessage("Format is: " + Format);
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
