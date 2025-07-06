using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.ChatFolder
{
    public class ChatCommand(Server server) : Command(server)
    {
        public override string Name => "Chat";
        public override string Description => "This will display Chat information";
        public override string Format => "[chatname]";
        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;


            var chat =
                server.Database.Chats.GetAll("Users","Messages").FirstOrDefault(x => x.Name.ToLower() == inputs[1].ToLower());


            if (chat == null)
            {
                Commander.SendErrorMessage("Chat does not exists");
                return;
            }

            Console.WriteLine($"""
                               <--- {chat.Name} --->
                               id: {chat.Id}
                               Name: {chat.Name}
                               Message Count: {chat.Messages.Count}
                               Creating Time: {chat.CreationOfCreation}
                               Last Time Updated: {chat.LastTimeEdited}
                               Users:
                               """);

            foreach (var user in chat.Users)
            {
                Console.WriteLine($"\t{user.UserName}");
            }

            Console.WriteLine("<--- --->");

            
            

        }
    }
}
