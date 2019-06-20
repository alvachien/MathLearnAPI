using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Quizuser
    {
        public Quizuser()
        {
            PermuserMonitorNavigation = new HashSet<Permuser>();
            PermuserUser = new HashSet<Permuser>();
        }


        [Key]
        public string Userid { get; set; }
        [Required]
        [StringLength(50)]
        public string Displayas { get; set; }
        public string Others { get; set; }
        public string Award { get; set; }
        public string Awardplan { get; set; }
        public bool? Deletionflag { get; set; }

        public ICollection<Permuser> PermuserMonitorNavigation { get; set; }
        public ICollection<Permuser> PermuserUser { get; set; }
    }
}
