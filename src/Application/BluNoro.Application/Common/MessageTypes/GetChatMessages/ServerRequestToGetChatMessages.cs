using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Abstracts;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Infrastructure.Logger;
using BluNoro.Core.Server.Infrastructure;

namespace BluNoro.Core.Common.MessageTypes.GetChatMessages
{
    public class ServerRequestToGetChatMessages : MessageBaseServer
    {
        public required Chat Chat { get; set; }

        public override void MessangeHandler(MessageServerManager serverManager)
        {
            serverManager.ChatController.GetChatMessages(this);


            //var database = serverManager.Database;
            //var serializer = serverManager.serializer;
            //var logger = serverManager.Logger;
            //var user = database.Users.GetFirst(x => x.Id == UserConnection.User.Id);

            //List<Message> messages = database.Messages.GetAll("UserConnection").Where(x => x.ParentChat.Id == Chat.Id).ToList();

            //ClientMultipleStringMessages clientMultiple = new ClientMultipleStringMessages()
            //{
            //    UserConnection = UserConnection
            //};
            //clientMultiple.Content = messages.ToList();
            //clientMultiple.idChat = Chat.Id;

            //string sendMultipleSeriazed = serializer.SerializeMessageToString(clientMultiple);

            //if (user.ServerStatus == null)
            //{
            //     logger.Add(LogFactory.UserNotFoundToSend(user));
            //     return;
            //}

            //if (user.ServerStatus.IsConnected == false)
            //{
            //     logger.Add(LogFactory.UserNotFoundToSend(user));
            //     return;
            //}


            //serverManager.Server.Send(UserConnection.ConnectionStatus.Adress.ToString(), sendMultipleSeriazed);

        }

        public override Message Convert()
        {
            throw new NotImplementedException();
        }


    }
}
