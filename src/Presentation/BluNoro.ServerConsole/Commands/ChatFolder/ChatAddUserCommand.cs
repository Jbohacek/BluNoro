using BluNoro.Core.Common.Entities;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.ChatFolder
{
    public class ChatAddUserCommand(Server server) : Command(server)
    {
        public override string Name => "ChatAddUser";
        public override string Description => "This will add user to Chat";
        public override string Format => "[chatName] [username]";
        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            if (!server.Database.Chats.exists(x => x.Name.ToLower() == inputs[1]))
            {
                Commander.SendErrorMessage("Chat does not exists");
                return;
            }

            if (!server.Database.Users.Exists(x => x.UserName.ToLower() == inputs[2]))
            {
                Commander.SendErrorMessage("user does not exists");
                return;
            }

            User user =
                server.Database.Users.GetFirst(x => x.UserName.ToLower() == inputs[2]);

            Chat chat =
                server.Database.Chats.GetAll("Users").First(x => x.Name.ToLower() == inputs[1].ToLower());

            if (chat.Users.Any(x => x == user))
            {
                Commander.SendErrorMessage("User alredy joined");
                return;
            }

            chat.Users.Add(user);

            server.Database.Chats.Update(chat);
            server.Database.Save();

            Commander.SendSuccessMessage("succesfully joined");
        }
    }
}
