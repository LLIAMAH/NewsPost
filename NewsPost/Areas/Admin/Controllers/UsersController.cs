using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace NewsPost.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(IRepUsers rep, ILogger<UsersController> logger)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
