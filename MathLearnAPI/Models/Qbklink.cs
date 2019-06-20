using System;
using System.Collections.Generic;

namespace MathLearnAPI.Models
{
    public partial class Qbklink
    {
        public int Qbid { get; set; }
        public int Kwgid { get; set; }

        public Knowledge Kwg { get; set; }
        public Questionbank Qb { get; set; }
    }
}
