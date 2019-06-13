using System;
using System.Collections.Generic;

namespace MathLearnAPI.Models
{
    public partial class Knowledge
    {
        public Knowledge()
        {
            Qbklink = new HashSet<Qbklink>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public ICollection<Qbklink> Qbklink { get; set; }
    }
}
