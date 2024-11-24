using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.Interfaces;
using QRMenu.Application.ViewModels;
using QRMenu.Core.Enums;

namespace QRMenu.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.GetByEmailAsync(model.Email);
                if (result.IsSuccess)
                {
                    var user = result.Data;

                    // Rol bazlı yönlendirme
                    string returnUrl = Url.Action("Index", "Home");
                    switch (user.Role)
                    {
                        case UserRole.Admin:
                            returnUrl = Url.Action("Index", "Admin");
                            break;
                        case UserRole.DealerAdmin:
                            returnUrl = Url.Action("Index", "Dealer");
                            break;
                        case UserRole.CompanyAdmin:
                            returnUrl = Url.Action("Index", "Company");
                            break;
                    }

                    // Authentication işlemleri burada...
                    return Redirect(returnUrl);
                }

                ModelState.AddModelError("", "Geçersiz email veya şifre");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Logout işlemleri burada...
            return RedirectToAction("Login");
        }
    }
}