using System;
using System.Collections.Generic;

namespace MathLearnAPI.Models
{
    public partial class Quizfaillog
    {
        public int Quizid { get; set; }
        public int Failidx { get; set; }
        public string Expected { get; set; }
        public string Inputted { get; set; }

        public Quiz Quiz { get; set; }
    }
}
