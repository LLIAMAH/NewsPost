using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPost.Areas.Admin.Models;
using NewsPost.Data.Entities;
using NewsPost.Data.LogData;
using NewsPost.Data.Reps;

namespace NewsPost.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IRepUsers _rep;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IRepUsers rep, UserManager<ApplicationUser> userManager, ILogger<UsersController> logger)
        {
            this._rep = rep;
            this._userManager = userManager;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                return View(this._rep.GetUserWithRoles());
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

        public async Task<IActionResult> AssignRole(string userId, ERole role)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));

                var user = _rep.GetUserById(userId);
                if (user != null)
                {
                    // Remove all roles instead of 'administrator'
                    var roles = Enum.GetNames(typeof(ERole));
                    foreach (var s in roles.Where(o => !o.Equals(ERole.Administrator.ToString())))
                        await _userManager.RemoveFromRoleAsync(user, s);

                    await _userManager.AddToRoleAsync(user, role.ToString());
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLogRecord(functionName, ex));
                return RedirectToAction(nameof(Index));
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }
    }
}
