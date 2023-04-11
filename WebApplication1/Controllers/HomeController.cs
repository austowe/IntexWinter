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

        public IActionResult Burial_Records(int pageNum = 1)
        {
            int pageSize = 10;

            var x = new BurialsViewModel
            {
                Burialmains = repo.Burialmains
                .OrderBy(b => b.Burialid)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBurials = repo.Burialmains.Count(),
                        //(bookCategory == null
                        //? repo.Burialmains.Count()
                        //: repo.Burialmains.Where(x => x.Category == bookCategory).Count()),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            return View(x);
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
