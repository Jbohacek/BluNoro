using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.ChatFolder
{
    public class ChatRemoveCommand(Server server) : Command(server)
    {
        public override string Name => "ChatRemove";
        public override string Description => "it will remove Chat and all messages";
        public override string Format => "[chatName]";
        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            var chat = server.Database.Chats.GetFirstOrDefault(x => x.Name.ToLower() == inputs[1].ToLower());
            if (chat == null)
            {
                Commander.SendErrorMessage("Chat does not exists");
                return;
            }

            if (!Commander.AskYesNo("Do you want to delete this Chat with all his messages?"))
                return;

            server.Database.Chats.Remove(chat);
            server.Database.Save();

            Commander.SendSuccessMessage("Chat has been removed");
        }
    }
}
