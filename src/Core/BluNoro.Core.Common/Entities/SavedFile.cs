using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Contracts.Interfaces;

namespace BluNoro.Core.Common.Entities
{
    [Table("tbSavedFile")]
    public class SavedFile : ITable
    {
        [Key] 
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]  
        [StringLength(500)]  
        public required string FilePath { get; set; }

        [Required] 
        public required long FileSize { get; set; }   

        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
