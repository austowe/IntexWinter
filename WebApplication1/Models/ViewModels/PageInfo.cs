using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumBurials { get; set; }
        public int BurialsPerPage { get; set; }
        public int CurrentPage { get; set; }

        // Calculate number of pages
        public int TotalPages => (int) Math.Ceiling((double)TotalNumBurials / BurialsPerPage);

        public string SelectedSex { get; set; }
        public string SelectedHairColor { get; set; }
        public string SelectedAgeAtDeath { get; set; }
        public string SelectedHeadDir { get; set; }
        public string SelectedDepth { get; set; }
    }
}
