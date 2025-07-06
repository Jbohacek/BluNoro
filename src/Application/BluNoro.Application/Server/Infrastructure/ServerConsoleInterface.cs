

//todo: Předělat

using System.Text;

namespace BluNoro.Core.Server.Infrastructure
{
    public class ServerConsoleInterface(Server server)
    {
        private Server Server { get; set; } = server;

        private StringBuilder sb = new StringBuilder();

        private void AddTitle(string title)
        {
            sb.Append($"<--- {title} --->\n");
        }

        public string GetStatus()
        {
            sb.Clear();
            AddTitle("Status server");
            TimeSpan? timespan = Server.ServerStartDate - DateTime.Now;
            if (timespan != null)
            {
                sb.Append("Server started on: " + Server.ServerStartDate.Value.ToLocalTime());
                sb.Append("\nIts on for: " + timespan);
            }

            sb.Append("-----");
            return sb.ToString();
        }

        public string GetUsersList()
        {
            sb.Clear();
            AddTitle("User connected");
            sb.Append("Guid\t\t\t\t\tAdress\t\t\tTimeSpendOn\tTimeOfJoin\n");

            Server.ConnectedUsers.ForEach(x =>
            {
                sb.Append($"{x.Id}\t{x.Adress}\t\t{x.ServerStatus.TimeOnServerFormatted()}\t{x.ServerStatus.TimeOfConnection.ToUniversalTime()}\n");
            });
            AddTitle("Anonymous users");
            sb.Append("ServerAdress\t\t\tTimeSpendOn\tTimeOfJoin\n");
            Server.AnonymousUsers.ForEach(x =>
            {
                sb.Append($"{x.Adress}\t\t{x.TimeOnServerFormatted()}\t{x.TimeOfConnection.ToUniversalTime()}\n");
            });

            return sb.ToString();
        }

    }
}
