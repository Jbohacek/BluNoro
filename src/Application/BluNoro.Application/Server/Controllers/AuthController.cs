using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Server.Infrastructure;
using BluNoro.Core.Common.MessageTypes.Authenticate;

namespace BluNoro.Core.Server.Controllers
{
    public class AuthController(MessageServerManager manager) : BaseServerController(manager)
    {
        public void VerifyUser(ServerVerification message)
        {
            User? possibleUser = _manager.Database.Users.GetAll("Chats").FirstOrDefault(x => x.UserName == message.UserName);

            //Check if user exists
            if (possibleUser == null)
            {
                _manager.Send(AuthFailed(message, "User not found"));
                _manager.Logger.Add(LogFactory.Authentication.UserNotFoundByUsername(message.UserName, message.UserConnection.IpPort));
                return;
            }

            //Check if password matches
            if (!PasswordManager.VerifyPassword(message.Password, possibleUser.HashPassword))
            {
                _manager.Send(AuthFailed(message, "Wrong password"));
                _manager.Logger.Add(LogFactory.Authentication.WrongPassword(message.UserName, message.UserConnection.IpPort));
                return;
            }

            //Check if user is already connected
            if (_manager.Parent.ConnectedUsers.Any(x => x.Id == possibleUser.Id))
            {
                _manager.Send(AuthFailed(message, "User already connected"));
                _manager.Logger.Add(LogFactory.Authentication.AlreadyConnected(possibleUser, message.UserConnection.IpPort));
                return;
            }

            message.UserConnection.User = possibleUser;

            //Generate responce
            var success = new ClientSuccessVerification()
            {
                UserConnection = message.UserConnection,
                AuthenticatedUser = possibleUser
            };

            _manager.Logger.Add(LogFactory.Authentication.Authenticated(possibleUser, message.UserConnection.IpPort));

            // Return to client
            _manager.Send(success);
            possibleUser.ServerStatus = message.UserConnection.ConnectionStatus;
            possibleUser.ServerStatus.IsConnected = true;

            //Remove anoymous and replace with user
            _manager.Parent.AnonymousUsers.RemoveAll(x => x.Adress.ToString() == message.UserConnection.ToString());
            _manager.Parent.ConnectedUsers.Add(possibleUser);

            _manager.Logger.Add(LogFactory.UserConnected(possibleUser));
        }

        private ClientFailedVerification AuthFailed(ServerVerification message, string reason)
        {
            return new ClientFailedVerification()
            {
                UserConnection = message.UserConnection,
                Reason = reason
            };
        }
    }
}
