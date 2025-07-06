using BluNoro.Core.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BluNoro.Core.Common.Entities
{
    [Table("tbChatMessages")]
    public class Message : ITable
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public Chat ParentChat { get; set; } = null!;
        [MaxLength(2000)]public string UnformatedMessage { get; set; } = null!;
        public User Sender { get; set; } = null!;

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime ArrivedTime { get; set; }

        public override string ToString()
        {
            return Sender.UserName + ": " +  UnformatedMessage;
        }

        public Message()
        {

        }
    }
}
