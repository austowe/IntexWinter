﻿using System;
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


        [HttpGet]
        public IActionResult Burial_Records(string sex = "z", string depth = "", string headdirection = "z", string ageatdeath = "z", string haircolor = "z", string wrapping = "z", int pageNum = 1)
        {
            int pageSize = 10;
            var x = new BurialsViewModel
            {
                Burialmains = repo.Burialmains
                .Where(b => b.Sex == sex || sex == null || sex == "z")
                .Where(b => b.Headdirection == headdirection || headdirection == null || headdirection == "z")
                .Where(b => b.Ageatdeath == ageatdeath || ageatdeath == null || ageatdeath == "z")
                .Where(b => b.Haircolor == haircolor || haircolor == null || haircolor == "z")
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBurials = (depth == null || depth == "")
                                    ? ((sex == null || sex == "z")
                                        ? ((headdirection == null || headdirection == "z")
                                            ? ((ageatdeath == null || ageatdeath == "z")
                                                ? ((haircolor == null || haircolor == "z")
                                                    ? repo.Burialmains.Count()
                                                    : repo.Burialmains.Where(b => b.Haircolor == haircolor).Count())
                                                : repo.Burialmains.Where(b => b.Ageatdeath == ageatdeath).Count())
                                            : repo.Burialmains.Where(b => b.Headdirection == headdirection).Count())
                                        : repo.Burialmains.Where(b => b.Sex == sex).Count())
                                    : repo.Burialmains.Where(b => b.Depth != "U" && b.Depth != null && b.Depth != "" && Convert.ToDecimal(b.Depth) <= Convert.ToDecimal(depth) && Convert.ToDecimal(b.Depth) > (Convert.ToDecimal(depth) - 1)).Count(),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };
            x.SelectedDepth = depth;
            x.SelectedSex = sex;
            x.SelectedHeadDir = headdirection;
            x.SelectedAgeAtDeath = ageatdeath;
            x.SelectedHairColor = haircolor;
            return View(x);
        }
    

        //Old Burial_Records version here while testing

        //public IActionResult Burial_Records(string burialSex, string hairColor, int pageNum = 1)
        //{
        //    int pageSize = 10;

        //    //Create new IQueryable so we can change display data without changing actual repo
        //    var BurialsIQeryable = repo.Burialmains;
        //    foreach (Burialmain burial in BurialsIQeryable)
        //    {
        //        //Change burial.Sex of BurialsIQeryable for display
        //        if (burial.Sex == null || burial.Sex == "")
        //        {
        //            burial.Sex = "N/A";
        //        } else if (burial.Sex == "M")
        //        {
        //            burial.Sex = "Male";
        //        } else if (burial.Sex == "F")
        //        {
        //            burial.Sex = "Female";
        //        }
        //    }


        //    var totalBurials = 0;

        //    if (burialSex == null)
        //    {
        //        totalBurials = BurialsIQeryable.Count();
        //    }
        //    else
        //    {
        //        totalBurials = BurialsIQeryable.Where(x => x.Sex == burialSex).Count();
        //    }


        //    var sexFilters = BurialsIQeryable
        //        .Select(x => x.Sex)
        //        .Distinct();


        //    var x = new BurialsViewModel
        //    {

        //    Burialmains = BurialsIQeryable
        //    //.Include(x=>x.burialmainTextiles).ThenInclude(y=>y.Textile).SingleOrDefault(m=>m.Id == id)
        //    .Where(b => b.Sex == burialSex || burialSex == null)
        //    .Where(b => b.Haircolor == hairColor || hairColor == null)
        //    .OrderBy(b => b.Id)
        //    .Skip((pageNum - 1) * pageSize)
        //    .Take(pageSize),

        //    PageInfo = new PageInfo
        //    {
        //        TotalNumBurials = totalBurials,
        //        BurialsPerPage = pageSize,
        //        CurrentPage = pageNum,
        //        sexFilters = sexFilters
        //    }
        //};

        //    return View(x);
        //}

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

                repo.Edit(burial);
                repo.SaveChanges();

                return RedirectToAction("Burial_Records");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Add_Burial()
        {
            var lastId = repo.Burialmains.OrderByDescending(x => x.Id).FirstOrDefault()?.Id ?? 0;
            var model = new Burialmain { Id = lastId + 1 };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add_Burial(Burialmain model)
        {
            if (ModelState.IsValid)
            {
                repo.Add_Burial(model);
                repo.SaveChanges();
                return RedirectToAction("Confirmation", new { id = model.Id });
            }
            else
            {
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult Delete(long id)
        {
            var burial = repo.Burialmains.Single(x => x.Id == id);

            return View(burial);
        }

        [HttpPost]
        public IActionResult Delete(Burialmain ar)
        {
            repo.Delete(ar);
            repo.SaveChanges();


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
