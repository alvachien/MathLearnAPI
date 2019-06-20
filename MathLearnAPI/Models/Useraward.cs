using System;
using System.Collections.Generic;

namespace MathLearnAPI.Models
{
    public partial class Useraward
    {
        public int Aid { get; set; }
        public string Userid { get; set; }
        public DateTime Adate { get; set; }
        public int Award { get; set; }
        public int? Planid { get; set; }
        public int? Qid { get; set; }
        public string Used { get; set; }
        public bool? Publish { get; set; }
    }
}
