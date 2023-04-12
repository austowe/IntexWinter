using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Models.ViewModels
{
    public class BurialsViewModel
    {
        public IQueryable<Burialmain> Burialmains { get; set; }
        public PageInfo PageInfo { get; set; }
        public string SelectedDepth { get; set; }
        public string SelectedSex { get; set; }
        public string SelectedHeadDir { get; set; }
        public string SelectedAgeAtDeath { get; set; }
        public string SelectedHairColor { get; set; }
    }
}