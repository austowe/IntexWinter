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

        public BurialDetailsViewModel BurialDetailsViewModel { get; set; }

    }
}