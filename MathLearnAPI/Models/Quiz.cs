using System;
using System.Collections.Generic;

namespace MathLearnAPI.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Quizfaillog = new HashSet<Quizfaillog>();
            Quizsection = new HashSet<Quizsection>();
        }

        public int Quizid { get; set; }
        public short Quiztype { get; set; }
        public string Basicinfo { get; set; }
        public string Attenduser { get; set; }
        public DateTime Submitdate { get; set; }

        public ICollection<Quizfaillog> Quizfaillog { get; set; }
        public ICollection<Quizsection> Quizsection { get; set; }
    }
}
