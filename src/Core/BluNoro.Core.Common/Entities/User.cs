using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using BluNoro.Core.Common.DataObjects;
using BluNoro.Core.Contracts.Enums;
using BluNoro.Core.Contracts.Interfaces;

namespace BluNoro.Core.Common.Entities
{
    [Table("tbUsers")]
    public class User : ITable
    {
        [Key]public Guid Id { get; set; }

        [MaxLength(30)] public string UserName { get; set; } = null!;
        [XmlIgnore,MaxLength(61)]public string HashPassword { get; set; } = null!;
        [DefaultValue("Default"),MaxLength(1000)] public string ProfilePicPath { get; set; } = "DefaultUserPic";
        [DefaultValue(Roles.Guest)] public Roles Role { get; set; }
        [DefaultValue(false)]public bool IsLocked { get; set; } = false;
        [DefaultValue("{}"),MaxLength(Int32.MaxValue)]public string UserOptions { get; set; } = "{}";

        public DateTime LastLogIn { get; set; } = DateTime.Now;
        public DateTime CreationTime { get; set; } = DateTime.Now;

        [XmlIgnore]public List<Chat> Chats { get; set; } = new List<Chat>();
        [XmlIgnore]public List<Message> Messages { get; set; } = new List<Message>();

        [XmlIgnore][NotMapped]public ConnectionStatus? ServerStatus { get; set; }
        [XmlIgnore][NotMapped]public IpPort? Adress => ServerStatus?.Adress;
        


        public User(IpPort adress, DateTime timeOfConnection)
        {
            ServerStatus = new ConnectionStatus(adress, timeOfConnection);
        }

        public User()
        {
        }

        public User(string ipPort, DateTime timeOfConnection) : this(new IpPort(ipPort), timeOfConnection)
        {

        }


    }
}
