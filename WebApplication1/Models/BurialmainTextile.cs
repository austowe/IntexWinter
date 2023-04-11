using System;
using System.Collections.Generic;

namespace IntexWinter.Models
{
    public partial class BurialmainTextile
    {
        public long MainBurialmainid { get; set; }
        public long MainTextileid { get; set; }

        public Burialmain Burialmain { get; set; }
        public Textile Textile { get; set; }
    }
}
