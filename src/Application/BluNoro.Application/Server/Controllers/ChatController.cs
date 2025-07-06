using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.MessageTypes.GetChatMessages;
using BluNoro.Core.Common.MessageTypes.GetChats;
using BluNoro.Core.Server.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BluNoro.Core.Server.Controllers
{
    public class ChatController(MessageServerManager manager) : BaseServerController(manager)
    {
        public void Handle(ServerRequestChats message)
        {
            List<Chat> chatList = Database.Chats
                .GetAll("Users")
                .Where(x => x.Users.Any(z => z.Id == message.UserId))
                .ToList();

            var responce = new ClientReturnChats()
            {
                UserConnection = message.UserConnection,
                Chats = chatList,
            };

            _manager.Send(responce);

        }

        public void GetChatMessages(ServerRequestToGetChatMessages message)
        {
            List<Message> messages = _manager.Database.Messages
                    .Include(x => x.ParentChat)
                    .Include(x => x.Sender)
                    .Where(x => x.ParentChat.Id == message.Chat.Id)
                    .Select(x => new Message()
                    {
                        Id = x.Id,
                        Sender = x.Sender,
                        UnformatedMessage = x.UnformatedMessage
                    })
                    .ToList();



            var responce = new ClientMultipleStringMessages()
            {
                UserConnection = message.UserConnection,
                Content = messages,
                idChat = message.Chat.Id
            };
            _manager.Send(responce);

            // Todo: proč jsem to tak napsal xd
            //var user = database.Users.GetFirst(x => x.Id == UserConnection.User.Id); 
            //List<Message> messages = database.Messages.GetAll("UserConnection").Where(x => x.ParentChat.Id == chat.Id).ToList();

            //ClientMultipleStringMessages clientMultiple = new ClientMultipleStringMessages()
            //{
            //    UserConnection = UserConnection
            //};
            //clientMultiple.Content = messages.ToList();
            //clientMultiple.idChat = chat.Id;

            //string sendMultipleSeriazed = serializer.SerializeMessageToString(clientMultiple);

            //if (user.ServerStatus == null)
            //{
            //    logger.Add(LogFactory.UserNotFoundToSend(user));
            //    return;
            //}

            //if (user.ServerStatus.IsConnected == false)
            //{
            //    logger.Add(LogFactory.UserNotFoundToSend(user));
            //    return;
            //}


            //serverManager.Server.Send(UserConnection.ConnectionStatus.Adress.ToString(), sendMultipleSeriazed);
        }
    }
}
