using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.ChatFolder
{
    public class ChatAddCommand(Server server) : Command(server)
    {
        public override string Name => "ChatAdd";
        public override string Description => "this will add Chat to database";
        public override string Format => "[Chatname]";
        public override void InvokeCommand(string[] inputs)
        {
            if(!CheckFormat(inputs)) return;

            Chat chat = new Chat(inputs[1]);

            if (server.Database.Chats.exists(x => x.Name.ToLower() == chat.Name.ToLower()))
            {
                Commander.SendErrorMessage("Chat already exists");
                return;
            }

            server.Database.Chats.Add(new Chat(inputs[1]));
            server.Database.Save();

            Commander.SendSuccessMessage($"{inputs[1]} succesfully added");

        }
    }
}
