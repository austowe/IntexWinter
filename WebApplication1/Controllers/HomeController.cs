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
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private intexContext _intexContext { get; set; }

        private static readonly HttpClient client = new HttpClient();

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
        [Authorize(Roles = "Researcher")]
        public IActionResult Edit(long id)
        {
            var burial = repo.Burialmains.Single(x => x.Id == id);
            return View("Edit", burial);
        }

        [HttpPost]
        [Authorize(Roles = "Researcher")]
        public IActionResult Edit(Burialmain burial)
        {
            if (ModelState.IsValid)
            {

                repo.Edit(burial);
                repo.SaveChanges();

                return RedirectToAction("Burial_Records");
            }
            else
            {
                return View(burial);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Researcher")]
        public IActionResult Add_Burial()
        {
            var lastId = repo.Burialmains.OrderByDescending(x => x.Id).FirstOrDefault()?.Id ?? 0L;
            var model = new Burialmain { Id = lastId + 1};

            return View("Add_Burial", model);
        }

        [HttpPost]
        [Authorize(Roles = "Researcher")]
        public IActionResult Add_Burial(Burialmain burialmain)
        {
            if (ModelState.IsValid)
            {
                repo.Add_Burial(burialmain);
                return RedirectToAction("Index");
            }
            else
            {
                return View(burialmain);
            }
        }



        [HttpGet]
        [Authorize(Roles = "Researcher")]
        public IActionResult Delete(long id)
        {
            var burial = repo.Burialmains.Single(x => x.Id == id);

            return View(burial);
        }

        [HttpPost]
        [Authorize(Roles = "Researcher")]
        public IActionResult Delete(Burialmain ar)
        {
            repo.Delete(ar);
            repo.SaveChanges();


            return RedirectToAction("Burial_Records");
        }

        [HttpGet]
        public IActionResult Predictions(PredictModel pm)
        {
            return View(pm);
        }

        [HttpPost]
        public async Task<IActionResult> PredictionsAsync(PredictModel pm)
        {
            IDictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("squarenorthsouth", pm.Northsouthsquare.ToString());
            payload.Add("depth", pm.Depth.ToString());
            payload.Add("squareeastwest", pm.Eastwestsquare.ToString());
            payload.Add("westtohead", pm.Westtohead.ToString());
            payload.Add("length", pm.Length.ToString());
            payload.Add("westtofeet", pm.Westtofeet.ToString());
            payload.Add("southtofeet", pm.Southtofeet.ToString());
            payload.Add("fieldbookexcavationyear", pm.Year.ToString());
            if (pm.Sex == "F")
            {
                payload.Add("sex_F", "1");
            }
            else
            {
                payload.Add("sex_F", "0");
            }
            if (pm.Eastwest == "W")
            {
                payload.Add("eastwest_E", "0");
                payload.Add("eastwest_W", "1");
            }
            else
            {
                payload.Add("eastwest_E", "1");
                payload.Add("eastwest_W", "0");
            }
            if (pm.Age == "A")
            {
                payload.Add("adultsubadult_A", "1");
                payload.Add("adultsubadult_C", "0");
            }
            else if (pm.Age == "U")
            {
                payload.Add("adultsubadult_A", "0");
                payload.Add("adultsubadult_C", "0");
            }
            else
            {
                payload.Add("adultsubadult_A", "0");
                payload.Add("adultsubadult_C", "1");
            }

            if (pm.Facebundles == "Y")
            {
                payload.Add("facebundles_Y", "1");
            }
            else
            {
                payload.Add("facebundles_Y", "0");
            }

            if (pm.Wrapping == "B")
            {
                payload.Add("wrapping_B", "1");
                payload.Add("wrapping_H", "0");
                payload.Add("wrapping_W", "0");
            }
            else if (pm.Wrapping == "W")
            {
                payload.Add("wrapping_B", "0");
                payload.Add("wrapping_H", "0");
                payload.Add("wrapping_W", "1");
            }
            else
            {
                payload.Add("wrapping_B", "0");
                payload.Add("wrapping_H", "1");
                payload.Add("wrapping_W", "0");
            }
            if (pm.Haircolor == "A")
            {
                payload.Add("haircolor_main_A", "1");
                payload.Add("haircolor_main_B", "0");
                payload.Add("haircolor_main_D", "0");
                payload.Add("haircolor_main_R", "0");
            }
            else if (pm.Haircolor == "B")
            {
                payload.Add("haircolor_main_A", "0");
                payload.Add("haircolor_main_B", "1");
                payload.Add("haircolor_main_D", "0");
                payload.Add("haircolor_main_R", "0");
            }
            else if (pm.Haircolor == "D")
            {
                payload.Add("haircolor_main_A", "0");
                payload.Add("haircolor_main_B", "0");
                payload.Add("haircolor_main_D", "1");
                payload.Add("haircolor_main_R", "0");
            }
            else if (pm.Haircolor == "R")
            {
                payload.Add("haircolor_main_A", "0");
                payload.Add("haircolor_main_B", "0");
                payload.Add("haircolor_main_D", "0");
                payload.Add("haircolor_main_R", "1");
            }
            else
            {
                payload.Add("haircolor_main_A", "0");
                payload.Add("haircolor_main_B", "0");
                payload.Add("haircolor_main_D", "0");
                payload.Add("haircolor_main_R", "0");
            }
            if (pm.Area == "NE")
            {
                payload.Add("area_NE", "1");
                payload.Add("area_NNW", "0");
                payload.Add("area_SE", "0");
                payload.Add("area_SW", "0");
            }
            else if (pm.Area == "SE")
            {
                payload.Add("area_NE", "0");
                payload.Add("area_NNW", "0");
                payload.Add("area_SE", "1");
                payload.Add("area_SW", "0");
            }
            else if (pm.Area == "SW")
            {
                payload.Add("area_NE", "0");
                payload.Add("area_NNW", "0");
                payload.Add("area_SE", "0");
                payload.Add("area_SW", "1");
            }

            else
            {
                payload.Add("area_NE", "0");
                payload.Add("area_NNW", "0");
                payload.Add("area_SE", "0");
                payload.Add("area_SW", "0");
            }
            if (pm.Age == "A")
            {
                payload.Add("ageatdeath_A", "1");
                payload.Add("ageatdeath_C", "0");
            }
            else if (pm.Age == "C")
            {
                payload.Add("ageatdeath_A", "0");
                payload.Add("ageatdeath_C", "1");
            }
            else
            {
                payload.Add("ageatdeath_A", "0");
                payload.Add("ageatdeath_C", "0");
            }

            if (pm.Function == "T")
            {
                payload.Add("function_value_Tunic", "1");
                payload.Add("function_value_Unknown", "0");
            }
            else if (pm.Function == "U")
            {
                payload.Add("function_value_Tunic", "0");
                payload.Add("function_value_Unknown", "1");
            }
            else
            {
                payload.Add("function_value_Tunic", "0");
                payload.Add("function_value_Unknown", "0");
            }

            if (pm.Thickness == "F")
            {
                payload.Add("thickness_F", "1");
            }
            else
            {
                payload.Add("thickness_F", "0");
            }

            if (pm.Angle == "M")
            {
                payload.Add("angle_Medium spun", "1");
                payload.Add("angle_Soft spun", "0");
            }
            else if (pm.Angle == "S")
            {
                payload.Add("angle_Medium spun", "0");
                payload.Add("angle_Soft spun", "1");
            }
            else
            {
                payload.Add("angle_Medium spun", "0");
                payload.Add("angle_Soft spun", "0");
            }

            if (pm.Manipulation == "WP")
            {
                payload.Add("manipulation_Warp", "1");
                payload.Add("manipulation_Weft", "0");
            }
            else if (pm.Manipulation == "WT")
            {
                payload.Add("manipulation_Warp", "0");
                payload.Add("manipulation_Weft", "1");
            }
            else
            {
                payload.Add("manipulation_Warp", "0");
                payload.Add("manipulation_Weft", "0");
            }

            if (pm.Material == "L")
            {
                payload.Add("material_Linen", "1");
                payload.Add("material_Wool", "0");
            }
            else if (pm.Material == "W")
            {
                payload.Add("material_Linen", "0");
                payload.Add("material_Wool", "1");
            }
            else
            {
                payload.Add("material_Linen", "0");
                payload.Add("material_Wool", "0");
            }

            if (pm.Yarndirection == "S")
            {                   
                payload.Add("direction_yarnmanipulation_S", "1");
            }
            else
            {
                payload.Add("direction_yarnmanipulation_S", "0");
            }

            var content = new FormUrlEncodedContent(payload);

            var response = await client.PostAsync("https://stowe.dev/intex/?apikey=XQDdgrTv8c4ZRsh1rUnAVbktzgz6Dwb9t3V55Kc93yB0chgFgwe9", content);

            var responseString = await response.Content.ReadAsStringAsync();
            //{response:"ok","prediction":"E"}
            var result = responseString[34].ToString();

            if (result == "W")
            {
                result = "West";
            }
            else if (result == "E")
            {
                result = "East";
            }
            else
            {
                result = "Indeterminate. This is rare, and it means that the data you've entered is consistent with one of the few individuals who were not facing neither east nor west when they were found.";
            }

            pm.Prediction = result;
            pm.Display = "block";
            //System.Diagnostics.Debug.WriteLine(responseString);

            return View(pm);
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
