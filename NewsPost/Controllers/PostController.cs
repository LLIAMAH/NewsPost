using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPost.Data.Entities;
using NewsPost.Data.LogData;
using NewsPost.Data.Reps;
using NewsPost.Helpers;
using NewsPost.Models;

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

        public IActionResult AddComment(long id = 0, CommentModel model = null) =>
            model == null || model.IsNull
                ? AddCommentById(id)
                : AddCommentByModel(model);

        public IActionResult AddCommentById(long id = 0)
        {
            var functionName= MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                if (id == 0)
                    throw new ArgumentNullException(nameof(id));

                var article = this._rep.GetArticle(id);
                if(!string.IsNullOrEmpty(article.Error))
                    throw new Exception(article.Error);

                var model = new CommentModel {Article = article.Data};
                return View("AddComment", model);
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

        public IActionResult AddCommentByModel(CommentModel model)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                return View("AddComment", model);
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


        [HttpPost(Name = "AddComment")]
        public IActionResult AddCommentSubmit(CommentModel model)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));

                ModelState.Remove("Article.Title");
                ModelState.Remove("Article.Author");
                ModelState.Remove("Article.Text");

                if (!ModelState.IsValid)
                {
                    return AddCommentByModel(new CommentModel()
                    {
                        Error = ModelState.GetErrors(),
                        Article = this._rep.GetArticle(model.Article.Id)?.Data
                    });
                }

                var article = this._rep.GetArticle(model.Article.Id);
                if (!string.IsNullOrEmpty(article.Error))
                    throw new Exception(article.Error);

                var result = this._rep.AddComment(article.Data.Id, model.Text);
                if (!string.IsNullOrEmpty(result.Error))
                {
                    return AddCommentByModel(new CommentModel()
                    {
                        Error = article.Error,
                        Article = article.Data,
                        Text = model.Text
                    });
                }

                return RedirectToAction(nameof(Index), "Home", null);
            }
            catch (Exception ex)
            {
                this._logger?.LogError(LogRecord.CreateLogRecord(functionName, ex));
                return RedirectToAction(nameof(Index), "Home", null);
            }
            finally
            {
                this._logger?.LogInformation(LogRecord.CreateLogFinish(functionName));
            }
        }
    }
}
