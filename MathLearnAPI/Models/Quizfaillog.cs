using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Quizfaillog
    {
        [Key]
        public int Quizid { get; set; }
        [Key]
        public int Failidx { get; set; }
        public string Expected { get; set; }
        public string Inputted { get; set; }

        public Quiz Quiz { get; set; }
    }
}
