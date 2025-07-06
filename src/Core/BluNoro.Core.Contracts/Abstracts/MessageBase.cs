using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Contracts.Interfaces;

namespace BluNoro.Core.Contracts.Abstracts
{
    public abstract class MessageBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime SendTime { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime RecievedTime { get; set; }

    }
}
