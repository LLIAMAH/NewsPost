using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPost.Areas.Admin.Models;
using NewsPost.Data.LogData;
using NewsPost.Data.Reps;

namespace NewsPost.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IRepUsers _rep;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IRepUsers rep, ILogger<UsersController> logger)
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
                var data = this._rep.GetUserWithRoles();
                return View(data);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLogRecord(functionName, ex));
                return View(new Result<IEnumerable<UserData>>(ex.Message));
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }

        }
    }
}
