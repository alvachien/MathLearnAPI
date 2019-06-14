using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MathLearnAPI.Models
{
    public partial class Awardplan
    {
        [Key]
        public int Planid { get; set; }
        public string Tgtuser { get; set; }
        public string Createdby { get; set; }
        public DateTime Validfrom { get; set; }
        public DateTime Validto { get; set; }
        public short Quiztype { get; set; }
        public string Quizcontrol { get; set; }
        public int? Minscore { get; set; }
        public int? Minavgtime { get; set; }
        public int Award { get; set; }
    }
}
