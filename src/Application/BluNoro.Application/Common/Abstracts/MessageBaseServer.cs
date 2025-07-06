using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Contracts.Abstracts;
using BluNoro.Core.Server.Infrastructure;

namespace BluNoro.Core.Common.Abstracts
{

    public abstract class MessageBaseServer : MessageBase
    {
        [XmlIgnore] Server.Server Server { get; set; } //Todo: Tady je něco zle!!!!

        public required UserConnection UserConnection { get; set; }

        public abstract void MessangeHandler(MessageServerManager serverManager);


        public virtual Message Convert()
        {
            throw new NotSupportedException("Chybí přeformátování na uložení");
        }

        public virtual void SaveMe()
        {
            Server.Database.Messages.Add(Convert());
            Server.Database.Save();
        }

    }
}
