using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Questionbank
    {
        public Questionbank()
        {
            Qbklink = new HashSet<Qbklink>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public byte[] Attachment1 { get; set; }
        public byte[] Attachment2 { get; set; }
        public byte[] Attachment3 { get; set; }
        public byte[] Attachment4 { get; set; }
        public byte[] Attachment5 { get; set; }

        public ICollection<Qbklink> Qbklink { get; set; }
    }
}
