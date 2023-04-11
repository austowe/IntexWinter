using IntexWinter.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Components
{
    public class SexesViewComponent : ViewComponent
    {
        public IIntexWinterRepository repo { get; set; }

        public SexesViewComponent (IIntexWinterRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedSex = RouteData?.Values["burialSex"];

            var sexes = repo.Burialmains
                .Select(x => x.Sex)
                .Distinct();

            return View(sexes);
        }
    }
}
