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
        public string Content { get; set; }
        public byte[] Attachment1 { get; set; }
        public byte[] Attachment2 { get; set; }
        public byte[] Attachment3 { get; set; }
        public byte[] Attachment4 { get; set; }
        public byte[] Attachment5 { get; set; }

        public ICollection<Qbklink> Qbklink { get; set; }
    }
}
