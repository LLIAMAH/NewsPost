using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPost.Data.Entities;
using NewsPost.Data.Reps;

namespace NewsPost.Controllers
{
    public class PostsController : Controller
    {
        private readonly IRep _rep;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IRep rep, ILogger<PostsController> logger)
        {
            this._rep = rep;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
