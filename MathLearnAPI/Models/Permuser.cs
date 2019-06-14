using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MathLearnAPI.Models
{
    public partial class Permuser
    {
        [Key, Column(Order = 0)]
        public string Userid { get; set; }
        [Key, Column(Order = 1)]
        public string Monitor { get; set; }
    }
}
