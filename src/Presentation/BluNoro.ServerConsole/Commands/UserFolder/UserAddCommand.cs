using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Infrastructure;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.UserFolder
{
    internal class UserAddCommand(Server server) : Command(server)
    {
        public override string Name => "UserAdd";
        public override string Description => "this will add user to database";
        public override string Format => "[UserName] [password] [password]";

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;


            if (inputs[2] != inputs[3])
            {
                Commander.SendErrorMessage("Passwords do not match!");
                return;
            }

            string username = inputs[1];
            string password = inputs[2];

            var user = new User();
            user.HashPassword = password;
            user.UserName = username;

            if (server.Database.Users.Exists(x => x.UserName == user.UserName))
            {
                Commander.SendErrorMessage($"User already with {user.UserName} exists");
                return;
            }

            user = user.HashUserPassword();

            server.Database.Users.Add(user);
            server.Database.Save();

            Commander.SendSuccessMessage($"{user.UserName} Succesfully added");
        }
    }
}
