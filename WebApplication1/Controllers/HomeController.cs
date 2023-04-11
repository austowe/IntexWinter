using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using IntexWinter.Models;
using IntexWinter.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private IIntexWinterRepository repo;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IIntexWinterRepository temp)
        {
            _logger = logger;
            repo = temp;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Burial_Records(int? id, string burialSex, int pageNum = 1)
        {
            int pageSize = 10;

            //Create new IQueryable so we can change display data without changing actual repo
            var BurialsIQeryable = repo.Burialmains;
            foreach (Burialmain burial in BurialsIQeryable)
            {
                //Change burial.Sex of BurialsIQeryable for display
                if (burial.Sex == null || burial.Sex == "")
                {
                    burial.Sex = "N/A";
                } else if (burial.Sex == "M")
                {
                    burial.Sex = "Male";
                } else if (burial.Sex == "F")
                {
                    burial.Sex = "Female";
                }
            }

            var x = new BurialsViewModel
            {

            Burialmains = BurialsIQeryable
            //.Include(x=>x.burialmainTextiles).ThenInclude(y=>y.Textile).SingleOrDefault(m=>m.Id == id)
            .Where(b => b.Sex == burialSex || burialSex == null)
            .OrderBy(b => b.Burialid)
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize),

            //Update pageNum and pageSize to show correct number of page tabs
            PageInfo = new PageInfo
            {
                TotalNumBurials =
                    (burialSex == null
                    ? BurialsIQeryable.Count()
                    : BurialsIQeryable.Where(x => x.Sex == burialSex).Count()),
                BurialsPerPage = pageSize,
                CurrentPage = pageNum
            }
        };

            return View(x);
        }


        public IActionResult Burial_Details()
        {
            return View();
        }

        public IActionResult Sex_Analysis()
        {
            return View();
        }

        public IActionResult Unsupervised()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
