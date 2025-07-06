using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Contracts.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BluNoro.Core.Infrastructure.Logger
{
    public static class LogFactory
    {
        public static Log ServerStarted(IpPort adress)
        {
            return new Log("Server has started", adress.ToString(), LogLevels.Level.Success);
        }

        public static Log UserConnected(User user)
        {
            return new Log("User have connected",user.Adress.ToString(), LogLevels.Level.Informal);
        }

        public static Log AnonymousUserConnected(IpPort adress)
        {
            return new Log("Anonymous user have connected",adress.ToString() , LogLevels.Level.Informal);
        }
        public static Log AnonymousUserDisconnected(IpPort adress)
        {
            return new Log("Anonymous user have disconected", adress.ToString(), LogLevels.Level.Informal);
        }

        public static Log UserDisconnected(User user)
        {
            return new Log("User have disconected", user.Adress.ToString(), LogLevels.Level.Informal);

        }

        public static Log UserNotFound(string? ipPort)
        {
            return new Log("User was not found", ipPort, LogLevels.Level.Warning);
        }

        public static Log UserNotFoundToSend(User user)
        {
            return new Log("User was not found connected to server to send message", user.UserName+" - "+user.Id , LogLevels.Level.ServerError);
        }

        public static Log ServerStopping()
        {
            return new Log("Server stopped gracefully",DateTime.Now.ToLocalTime().ToString(), LogLevels.Level.ServerError );
        }

        public static Log ContextChange(EntityEntry entryChanged)
        {
            return new Log(entryChanged.State.ToString(), entryChanged.Entity.GetType().ToString(), LogLevels.Level.Informal);
        }

        public static Log StringMessageRecieved(User sender, string content)
        {
            return new Log(content, sender.UserName, LogLevels.Level.Informal);
        }

        public class Authentication
        {
            public static Log UserNotFoundByUsername(string username, string ipPort)
            {
                return new Log("User not found when authentication", username + " - " + ipPort);
            }

            public static Log WrongPassword(string username, string ipPort)
            {
                return new Log("User wrong Password", $"{username} - {ipPort}");
            }

            public static Log Authenticated(User user, string ipPort)
            {
                return new Log("User authenticated " + user.Id, $"{ipPort}");
            }

            public static Log AlreadyConnected(User user, string ipPort)
            {
                return new Log("User already connected " + user.Id + $" ({user.UserName})", $"{ipPort}");
            }
        }
    }
}