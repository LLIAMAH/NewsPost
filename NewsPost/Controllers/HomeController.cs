using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPost.Models;
using System;
using System.Diagnostics;
using NewsPost.Data.Reps;

namespace NewsPost.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepNews _rep;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IRepNews rep, ILogger<HomeController> logger)
        {
            this._rep = rep;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            var result = _rep.GetNewsDaily(DateTime.Now);
            return string.IsNullOrEmpty(result.Error) 
                ? View(result) 
                : View(default);
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
