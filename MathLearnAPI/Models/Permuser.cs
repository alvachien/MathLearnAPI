using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Permuser
    {
        [Key]
        public string Userid { get; set; }
        [Key]
        public string Monitor { get; set; }

        public Quizuser MonitorNavigation { get; set; }
        public Quizuser User { get; set; }
    }
}
