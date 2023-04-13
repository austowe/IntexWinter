using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Models.ViewModels
{
    public class SummaryView
    {
        public long id { get; set; }
        public string sex { get; set; }
        public string hairColor { get; set; }
        public string age { get; set; }
        public string headDir { get; set; }
        public string depth { get; set; }
        public List<TextileDetails> textileList  { get; set; }
    }
}
