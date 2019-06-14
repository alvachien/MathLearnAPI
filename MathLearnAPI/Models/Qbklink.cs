using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MathLearnAPI.Models
{
    public partial class Qbklink
    {
        [Key, Column(Order = 0)]
        public int Qbid { get; set; }
        [Key, Column(Order = 1)]
        public int Kwgid { get; set; }

        public Knowledge Kwg { get; set; }
        public Questionbank Qb { get; set; }
    }
}
