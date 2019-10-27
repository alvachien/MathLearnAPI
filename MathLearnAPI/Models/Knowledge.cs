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

        [Key]
        public int Id { get; set; }
        public byte? Category { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public bool? CanGenerate { get; set; }

        public ICollection<Qbklink> Qbklink { get; set; }

        public void CopyForCreate(Knowledge other)
        {
            if (other.Category.HasValue)
                this.Category = other.Category.Value;
            else
                this.Category = null;
            this.Name = other.Name;
            this.Content = other.Content;
            if (other.CanGenerate.HasValue)
                this.CanGenerate = other.CanGenerate.Value;
            else
                this.CanGenerate = null;
        }
    }
}
