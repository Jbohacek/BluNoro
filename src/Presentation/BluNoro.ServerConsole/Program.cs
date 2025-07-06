using System.Reflection;
using System.Text;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Data.EF.Context;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Server;
using BluNoro.Core.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BluNoro.ServerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server.ServerBuilder serverBuild = new Server.ServerBuilder();
            
            serverBuild.SetAdress(new IpPort("127.0.0.1", 9000));
            serverBuild.SetLogger(new Logger());
            serverBuild.SetDatabase(new SqlLiteContext("BluChat"));
            serverBuild.SetAdminUserPassword("123456");
            serverBuild.SetOnClosingEvent();

            Server server = serverBuild.Build();
            server.Start();


            // Vytvoření testovacích prostředků
            if (!server.Database.Chats.GetAll().Any(x => x.Name == "TestChat"))
            {
                Chat chat = new Chat("TestChat");
                chat.AddUserToChat(server.Database.Users.GetAdmin());
                server.Database.Chats.Add(chat);
                server.Database.Save();
            }


            Task.Run(() => { HandleInputs(server); });

            while (true)
            {
                Thread.Sleep(100);
            }
        }

        static void HandleInputs(Server server)
        {
            Commander commander = new Commander(server);

            ServerConsoleInterface consoleInterface = new ServerConsoleInterface(server);

            while (true)
            {
                string? input = Console.ReadLine();
                if(string.IsNullOrEmpty(input)) continue;

                input = input.Trim().ToLower();
                string[] inputs = input.Contains(' ') ? input.Split(' ') : new[] { input };
                
                Command command = commander.FindCommand(inputs[0]);

                if (inputs is [_, "?"])
                {
                    Console.WriteLine(command.Name + " >> " + command.Format  + "\n" + command.Description);
                    continue;
                }

                command.InvokeCommand(inputs);

                //    case "send":
                //        if (inputs.Length <= 2) continue;
                //        IpPort adress = new IpPort(inputs[1]);
                //        User user = server.ConnectedUsers.Find(x => adress.ToString() == x.ServerAdress.ToString());
                //        StringBuilder message = new StringBuilder();
                //        for (int i = 2; i < inputs.Length; i++)
                //        {
                //            message.Append(inputs[i]);
                //        }

                //        server.Send(user, message.ToString());
                //        break;



                //        break;


                //}

            }
        }

    }
}
