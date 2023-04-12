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
        private intexContext _intexContext { get; set; }

        private IIntexWinterRepository repo;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IIntexWinterRepository temp, intexContext context)
        {
            _intexContext = context;
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
        public IActionResult Burial_Details(long id)
        {
            var burial = repo.GetById(id);
            if (burial == null)
            {
                return NotFound();
            }

            var viewModel = new BurialDetailsViewModel(burial);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var burial = repo.GetById(id);
            var viewModel = new BurialEditViewModel
            {
                Id = burial.Id,
                SquareNorthSouth = burial.Squarenorthsouth,
                HeadDirection = burial.Headdirection,
                Sex = burial.Sex,
                NorthSouth = burial.Northsouth,
                Depth = burial.Depth,
                EastWest = burial.Eastwest,
                AdultSubadult = burial.Adultsubadult,
                FaceBundles = burial.Facebundles,
                SouthToHead = burial.Southtohead,
                Preservation = burial.Preservation,
                FieldBookPage = burial.Fieldbookpage,
                SquareEastWest = burial.Squareeastwest,
                Goods = burial.Goods,
                Text = burial.Text,
                Wrapping = burial.Wrapping,
                HairColor = burial.Haircolor,
                WestToHead = burial.Westtohead,
                SamplesCollected = burial.Samplescollected,
                Burialid = burial.Burialid,
                Area = burial.Area,
                Length = burial.Length,
                BurialNumber = burial.Burialnumber,
                DataExpertInitials = burial.Dataexpertinitials,
                WestToFeet = burial.Westtofeet,
                AgeAtDeath = burial.Ageatdeath,
                SouthToFeet = burial.Southtofeet,
                ExcavationRecorder = burial.Excavationrecorder,
                Photos = burial.Photos,
                Hair = burial.Hair,
                BurialMaterials = burial.Burialmaterials,
                DateOfExcavation = burial.Dateofexcavation,
                FieldBookExcavationYear = burial.Fieldbookexcavationyear,
                ClusterNumber = burial.Clusternumber,
                ShaftNumber = burial.Shaftnumber
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(BurialEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var burial = viewModel.BurialMain;

                repo.Update(burial);
                repo.SaveChanges();

                return RedirectToAction("Burial_Records");
            }
            else
            {
                return View(viewModel);
            }
        }



        [HttpGet]
        public IActionResult Delete(int burialid)
        {
            var burial = _intexContext.Burialmain.Single(x => x.Id == burialid);

            return View(burial);
        }

        [HttpPost]
        public IActionResult Delete(Burialmain ar)
        {
            _intexContext.Burialmain.Remove(ar);
            _intexContext.SaveChanges();

            return RedirectToAction("Burial_Records");
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
