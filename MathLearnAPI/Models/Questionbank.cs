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
        public byte Category { get; set; }
        [StringLength(50)]
        public string BriefCont { get; set; }
        [Required]
        public string Content { get; set; }
        public string Answer { get; set; }

        public ICollection<Qbklink> Qbklink { get; set; }
    }
}
