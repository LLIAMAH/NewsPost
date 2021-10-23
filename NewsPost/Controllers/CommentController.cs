using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPost.Data.LogData;
using NewsPost.Data.Reps;
using NewsPost.Helpers;
using NewsPost.Models;

namespace NewsPost.Controllers
{
    public class CommentController : Controller
    {
        private readonly IRepComments _rep;
        private readonly ILogger<CommentController> _logger;

        public CommentController(IRepComments rep, ILogger<CommentController> logger)
        {
            this._rep = rep;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddComment(long id = 0, CommentModel model = null) =>
            model == null || model.IsNull
                ? AddCommentById(id)
                : AddCommentByModel(model);

        public IActionResult AddCommentById(long id = 0)
        {
            var functionName = MethodBase.GetCurrentMethod()?.Name;
            try
            {
                this._logger?.LogInformation(LogRecord.CreateLogStart(functionName));
                if (id == 0)
                    throw new ArgumentNullException(nameof(id));

                var article = this._rep.GetArticle(id);
                if (!string.IsNullOrEmpty(article.Error))
                    throw new Exception(article.Error);

                var model = new CommentModel { Article = article.Data };
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
