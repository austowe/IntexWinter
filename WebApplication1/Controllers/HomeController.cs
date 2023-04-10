using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize(AuthenticationSchemes = "Identity.Application")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = "Identity.Application")]
        public IActionResult Burial_Summary()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = "Identity.Application")]
        public IActionResult Burial_Records()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = "Identity.Application")]
        public IActionResult Sex_Analysis()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = "Identity.Application")]
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
