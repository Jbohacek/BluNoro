using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.Services;
using BluNoro.Core.Contracts.Interfaces;
using BluNoro.Core.Data.EF.Context;

namespace BluNoro.Core.Data.EF.Repositories
{
    public class MessageRepository(SqlLiteContext context) : Repo<Message>(context)
    {
        private SqlLiteContext _context = context;

        public override void Add(Message item)
        {
            item.ParentChat = _context.Chats.First(x => x.Id == item.ParentChat.Id);
            item.Sender = _context.Users.First(x => x.Id == item.Sender.Id);

            _context.Messages.Add(item);
        }
    }
}
