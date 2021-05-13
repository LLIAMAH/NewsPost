using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using NewsPost.Areas.FirstStep.Models;
using NewsPost.Data.Entities;
using NewsPost.Data.Reps;

namespace NewsPost.Areas.FirstStep.Controllers
{
    [Area("FirstStep")]
    public class FirstStartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<CreateModel> _logger;

        public FirstStartController(SignInManager<ApplicationUser> signInManager,
            ILogger<CreateModel> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateModel()
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateModel model)
        {
            var returnUrl = model.ReturnUrl ?? Url.Content("~/");
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                await _roleManager.CreateAsync(new IdentityRole {Name = ERole.Administrator.ToString(), NormalizedName = ERole.Administrator.ToString()});
                await _roleManager.CreateAsync(new IdentityRole {Name = ERole.Editor.ToString(), NormalizedName = ERole.Editor.ToString()});
                await _roleManager.CreateAsync(new IdentityRole {Name = ERole.Writer.ToString(), NormalizedName = ERole.Writer.ToString()});

                var resultCreated = await _userManager.CreateAsync(user, model.Password);
                var resultAddRole = await _userManager.AddToRoleAsync(user, ERole.Administrator.ToString());

                if (resultCreated.Succeeded && resultAddRole.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var confirmedEmail = await _userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)));
                    if (confirmedEmail.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Index", "Home", new {Area = ""});
                    }

                    AddErrorsToModelState(ModelState, confirmedEmail);
                }

                AddErrorsToModelState(ModelState, resultCreated);
                AddErrorsToModelState(ModelState, resultAddRole);
            }

            return View();
        }

        private static void AddErrorsToModelState(ModelStateDictionary modelState, IdentityResult identityResult)
        {
            foreach (var error in identityResult.Errors)
                modelState.AddModelError(string.Empty, error.Description);
        }
    }
}
