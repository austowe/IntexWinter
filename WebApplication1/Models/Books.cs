using System;
using System.Collections.Generic;

namespace IntexWinter.Models
{
    public partial class Books
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
    }
}
