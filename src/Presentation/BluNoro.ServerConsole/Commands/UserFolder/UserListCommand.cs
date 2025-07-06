using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.UserFolder
{
    public class UserListCommand(Server server) : Command(server)
    {
        public override string Name => "UserList";
        public override string Description => "This will list every user in database";
        public override string Format => "";

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            Console.WriteLine($"<--- UserList --->");
            List<User> users = server.Database.Users.GetAll().ToList();
            Console.WriteLine("Count: " + users.Count);
            Console.WriteLine("<---\t--->");
            foreach (var user in users)
            {
                var isConnected = "Not connected";
                if (user.ServerStatus != null)
                    if(user.ServerStatus.IsConnected) 
                        isConnected = "Connected";
                
                Console.WriteLine($"{user.Id}\t{user.UserName, -15}\t{isConnected,-15}\t");
            }

            Console.WriteLine("<--- End --->");
        }
    }
}
