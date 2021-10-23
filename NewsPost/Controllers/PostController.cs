using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPost.Data.Entities;
using NewsPost.Data.LogData;
using NewsPost.Data.Reps;

namespace NewsPost.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IRepPosts _rep;
        private readonly ILogger<PostController> _logger;

        public PostController(IRepPosts rep, ILogger<PostController> logger)
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
                var data = _rep.GetUnApprovedArticles(_rep.GetUserId(this.User));
                if (!string.IsNullOrEmpty(data?.Error))
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
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Article model)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                var currentUserId = _rep.GetUserId(this.User);
                var currentDateTime = DateTime.Now;
                model.DateCreated = currentDateTime;
                model.AuthorId = currentUserId;
                if (this.User.IsInRole(ERole.Editor.ToString()))
                {
                    model.ApprovedById = currentUserId;
                    model.DateApproved = currentDateTime;
                }

                var result = _rep.Create(model);
                if (!string.IsNullOrEmpty(result.Error))
                    throw new Exception(result.Error);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLogRecord(functionName, ex));
                return RedirectToAction(nameof(Create), model);
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }

        public IActionResult Approve(long id = 0)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName)); 
                if (id == 0)
                    throw new ArgumentNullException(nameof(id));

                var result = _rep.Approve(id, _rep.GetUserId(this.User));
                if (!string.IsNullOrEmpty(result.Error))
                    throw new Exception(result.Error);

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
