using BluNoro.Core.Contracts.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;


namespace BluNoro.Core.Common.Entities
{
    [Table("tbChats")]
    public class Chat : ITable
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = null!;

        [XmlIgnore]public List<User> Users { get; set; } = new List<User>();
        [XmlIgnore]public List<Message> Messages { get; set; } = new List<Message>();

        [DefaultValue("Default"),MaxLength(1000)] public string ChatPicPath { get; set; } = "DefaultChat";

        [DefaultValue("{}"), MaxLength(Int32.MaxValue)]
        public string ChatOptions { get; set; } = "{}";

        public DateTime CreationOfCreation { get; set; } = DateTime.Now;

        public DateTime LastTimeEdited { get; set; } = DateTime.Now;


        public Chat()
        {
            
        }

        public Chat(string name)
        {
            Name = name;
        }

        public bool AddUserToChat(User user)
        {
            if (Users.Contains(user)) return false;
            Users.Add(user);
            LastTimeEdited = DateTime.Now;
            return true;
        }
        public bool RemoveUserFromChat(User user)
        {
            if (!Users.Contains(user)) return false;
            Users.Remove(user);
            LastTimeEdited = DateTime.Now;
            return true;
        }

        public bool AddMessageToChat(Message message)
        {
            if (Messages.Contains(message)) return false;
            Messages.Add(message);
            LastTimeEdited = DateTime.Now;
            return true;
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
