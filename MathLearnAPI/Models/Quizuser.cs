using System;
using System.Collections.Generic;

namespace MathLearnAPI.Models
{
    public partial class Quizuser
    {
        public string Userid { get; set; }
        public string Displayas { get; set; }
        public string Others { get; set; }
        public string Award { get; set; }
        public string Awardplan { get; set; }
        public bool? Deletionflag { get; set; }
    }
}
