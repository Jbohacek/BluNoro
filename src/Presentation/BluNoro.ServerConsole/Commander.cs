using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;
using BluNoro.Core.Server.Infrastructure;
using BluNoro.ServerConsole.Commands;

namespace BluNoro.ServerConsole
{
    public class Commander
    {
        private Server Server { get; set; }
        private ServerConsoleInterface ConsoleInterface { get; set; }


        List<Command> Commands = new List<Command>();

        public Commander(Server server)
        {
            ConsoleInterface = new ServerConsoleInterface(server);
            Server = server;

            LoadCommands();

        }

        private void LoadCommands()
        {
            // Get all types in the current assembly that inherit from Command
            var commandTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Command)));

            foreach (var type in commandTypes)
            {
                // Create an instance of each Command subclass
                if (Activator.CreateInstance(type, Server) is Command commandInstance)
                {
                    Commands.Add(commandInstance);
                }
            }
        }

        public Command FindCommand(string name)
        {
            name = name.ToLower().Trim();
            return Commands.FirstOrDefault(x => x.Name.ToLower() == name, new NotFoundCommand(Server));
        }

        public static void SendErrorMessage(string message) => SendColoredMessage(message, ConsoleColor.Red);
        public static void SendSuccessMessage(string message) => SendColoredMessage(message, ConsoleColor.Green);

        private static void SendColoredMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(">> ");
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static bool AskYesNo(string question = "")
        {
            do
            {
                if (question != "")
                    Console.WriteLine($"{question} y/n");
                else
                    Console.WriteLine("Are you sure? y/n");

                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    if (input.ToLower() == "y")
                    {
                        return true;
                    }
                    else if (input.ToLower() == "n")
                    {
                        return false;
                    }
                }
                SendErrorMessage("y/n");

            } while (true);
        }

    }
}
