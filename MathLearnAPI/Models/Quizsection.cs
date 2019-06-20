using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Quizsection
    {
        [Key]
        public int Quizid { get; set; }
        [Key]
        public int Section { get; set; }
        public int Timespent { get; set; }
        public int Totalitems { get; set; }
        public int Faileditems { get; set; }

        public Quiz Quiz { get; set; }
    }
}
