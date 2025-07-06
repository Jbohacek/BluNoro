using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Server;

namespace BluNoro.ServerConsole.Commands.UserFolder
{
    internal class UserImportCommand(Server server) : Command(server)
    {
        public override string Name => "UserImport";
        public override string Description => "Imports users from a CSV file. Format csv file like \"username,password\"";
        public override string Format => "[path] ['y'/'n':askIfWantToAdd]";

        public override void InvokeCommand(string[] inputs)
        {
            if (!CheckFormat(inputs)) return;

            string filePath = inputs[1];
            bool askQuestion = inputs[2] == "y" ? true : false;

            if (!File.Exists(filePath))
            {
                Commander.SendErrorMessage($"File not found: {filePath}");
                return;
            }

            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');

                    if (parts.Length < 2)
                    {
                        Commander.SendErrorMessage($"Invalid line format: {line}");
                        continue;
                    }

                    string username = parts[0].Trim();
                    string password = parts[1].Trim();

                    if (server.Database.Users.Exists(x => x.UserName == username))
                    {
                        Commander.SendErrorMessage($"User already exists: {username}");
                        continue;
                    }

                    if(askQuestion)
                        if(!Commander.AskYesNo($"Do you want to add user '{username}'?"))
                        {
                            Commander.SendErrorMessage($"Skipped user: {username}");
                            continue;
                        }

                    User user = new User
                    {
                        UserName = username,
                        HashPassword = password
                    };

                    server.Database.Users.Add(user);
                    Commander.SendSuccessMessage($"User '{username}' added successfully.");
                }

                server.Database.Save();
                Commander.SendSuccessMessage("User import completed.");
            }
            catch (Exception ex)
            {
                Commander.SendErrorMessage($"An error occurred: {ex.Message}");
            }
        }
    }
}
