using System;
using System.Collections.Generic;

namespace MathLearnAPI.Models
{
    public partial class Quizsection
    {
        public int Quizid { get; set; }
        public int Section { get; set; }
        public int Timespent { get; set; }
        public int Totalitems { get; set; }
        public int Faileditems { get; set; }

        public Quiz Quiz { get; set; }
    }
}
