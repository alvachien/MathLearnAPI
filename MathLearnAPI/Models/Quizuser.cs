using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Quizuser
    {
        [Key]
        public string Userid { get; set; }
        [Required]
        public string Displayas { get; set; }
        public string Others { get; set; }
        public string Award { get; set; }
        public string Awardplan { get; set; }
        public bool? Deletionflag { get; set; }
    }
}
