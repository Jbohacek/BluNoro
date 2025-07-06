using System;
using System.Linq;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.UserFolder
{
    public class UserCommand(Server server) : Command(server)
    {
        public override string Name => "User";
        public override string Description => "Shows the user information and where the user is";
        public override string Format => "[username]";


        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            string username = inputs[1].ToLower();

            User? user = server.Database.Users.GetAll("Chats")
                .FirstOrDefault(x => x.UserName.ToLower() == username);

            if (user == null)
            {
                Commander.SendErrorMessage("User does not exist.");
                return;
            }

            Console.WriteLine($"""
                               <--- {user.UserName} --->
                               id: {user.Id}
                               Name: {user.UserName}
                               Groups:
                               """);

          
            if (!user.Chats.Any())
            {
                Console.WriteLine("\tNo groups found.");
            }
            else
            {
                foreach (var chat in user.Chats)
                {
                    Console.WriteLine($"\t{chat.Name}");
                }
            }

            Console.WriteLine("<--- --->");
        }
    }
}

