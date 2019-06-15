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
        [StringLength(50)]
        public string Attachment1 { get; set; }
        [StringLength(50)]
        public string Attachment2 { get; set; }
        [StringLength(50)]
        public string Attachment3 { get; set; }
        [StringLength(50)]
        public string Attachment4 { get; set; }
        [StringLength(50)]
        public string Attachment5 { get; set; }

        public ICollection<Qbklink> Qbklink { get; set; }
    }
}
