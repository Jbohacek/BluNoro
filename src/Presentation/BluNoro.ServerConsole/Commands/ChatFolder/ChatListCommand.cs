using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.ChatFolder
{
    public class ChatListCommand(Server server) : Command(server)
    {
        public override string Name => "ChatList";
        public override string Description => "This will display list of chats saved on server";
        public override string Format => "";
        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;
            Console.WriteLine("<--- Chat List --->");
            Console.WriteLine("ID\t\t\t\t\tChatName\tUserCount");

            var chats = server.Database.Chats.GetAll("Users");

            foreach (var chat in chats)
            {
                Console.WriteLine($"{chat.Id}\t{chat.Name,-15}\t{chat.Users.Count}");
            }

            Console.WriteLine("<--- --->");
        }
    }
}
