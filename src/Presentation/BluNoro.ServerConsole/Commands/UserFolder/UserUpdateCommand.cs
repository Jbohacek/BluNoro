using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Infrastructure;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.UserFolder
{
    public class UserUpdateCommand(Server server) : Command(server)
    {
        public override string Name => "UserUpdate";
        public override string Description => "this will update user information";
        public override string Format => "[username] ['username'or'password'] [NewValue]";

        private string Username { get; set; } = "";
        private User? User { get; set; } = null;

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            string typeInput = inputs[2];
            Username = inputs[1];


            User = server.Database.Users.GetFirstOrDefault(x => x.UserName.ToLower() == Username.ToLower());
            if (User == null)
            {
                Commander.SendErrorMessage("User not found");
                return;
            }


            switch (typeInput.ToLower())
            {
                case "username":
                    UserNameUpdate(inputs[3]);
                    break;
                case "password":
                    PasswordUpdate(inputs[3]);
                    break;
                default:
                    Commander.SendErrorMessage("Format is: [username] ['username' or 'password'] [new value]");
                    break;
            }
        }

        private void PasswordUpdate(string value)
        {
            var user = server.Database.Users.GetFirst(x => x.UserName == Username);
            user.HashPassword = value;
            user = user.HashUserPassword();
            server.Database.Users.Update(user);
            server.Database.Save();
            Commander.SendSuccessMessage("Password updated");
        }

        private void UserNameUpdate(string value)
        {
            User.UserName = value;
            server.Database.Users.Update(User);
            server.Database.Save();
            Commander.SendSuccessMessage($"Username Updated ({Username} > {value})");
        }
    }
}
