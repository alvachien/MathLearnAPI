using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Qbklink
    {
        [Key]
        public int Qbid { get; set; }
        [Key]
        public int Kwgid { get; set; }

        public Knowledge Kwg { get; set; }
        public Questionbank Qb { get; set; }
    }
}
