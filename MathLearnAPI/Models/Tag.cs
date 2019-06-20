using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Tag
    {
        [Key]
        public string Tag1 { get; set; }
        [Key]
        public short Reftype { get; set; }
        [Key]
        public int Refid { get; set; }
    }
}
