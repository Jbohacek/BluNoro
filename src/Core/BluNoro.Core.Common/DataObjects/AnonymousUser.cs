using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;

namespace BluNoro.Core.Common.DataObjects
{
    public sealed class AnonymousUser : User
    {
        public AnonymousUser()
        {
            this.Id = Guid.NewGuid();
            this.UserName = "Anonymous";
        }
    }
}
