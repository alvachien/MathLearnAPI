using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Knowledge
    {
        public Knowledge()
        {
            Qbklink = new HashSet<Qbklink>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }

        public ICollection<Qbklink> Qbklink { get; set; }
    }
}
