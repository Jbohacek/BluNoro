using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.Services;
using BluNoro.Core.Data.EF.Context;

namespace BluNoro.Core.Data.EF.Repositories
{
    public class ChatRepository(SqlLiteContext context) : Repo<Chat>(context)
    {
        private readonly SqlLiteContext _context = context;

        public override void Add(Chat item)
        {
            if (exists(x => x.Name.ToLower() == item.Name.ToLower()))
            {
                throw new Exception("Already exists");
            }
            base.Add(item);
        }

        public bool exists(Func<Chat, bool> predicate)
        {
            return _context.Chats.Any(predicate);
        }

        public override void Update(Chat item)
        {
            item.LastTimeEdited = DateTime.Now;
            base.Update(item);
        }
    }
}

