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
    [Table("tbAttachments")]
    public class Attachment : ITable
    {
        [Key]public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(250)]public required string FileName { get; set; } 
        [MaxLength(100)]public required string FileType { get; set; }  
        [Required]public Message Message { get; set; } = null!;
        [Required]public SavedFile SavedFile { get; set; } = null!;
    }
}
