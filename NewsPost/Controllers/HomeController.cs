using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPost.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using NewsPost.Data.Entities;
using NewsPost.Data.LogData;
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
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                var data = this._rep.GetNewsDaily(DateTime.Now);
                if (string.IsNullOrEmpty(data?.Error))
                    throw new Exception(data?.Error);

                return View(data);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLogRecord(functionName, ex));
                return View(new Result<IEnumerable<Article>>(ex.Message));
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
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
