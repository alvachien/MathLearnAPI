using System;
using System.Collections.Generic;

namespace MathLearnAPI.Models
{
    public partial class Questionbank
    {
        public Questionbank()
        {
            Qbklink = new HashSet<Qbklink>();
        }

        public int Id { get; set; }
        public byte Category { get; set; }
        public string BriefCont { get; set; }
        public string Content { get; set; }
        public string Attachment1 { get; set; }
        public string Attachment2 { get; set; }
        public string Attachment3 { get; set; }
        public string Attachment4 { get; set; }
        public string Attachment5 { get; set; }

        public ICollection<Qbklink> Qbklink { get; set; }
    }
}
