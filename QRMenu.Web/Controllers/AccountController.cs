using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.Interfaces;
using QRMenu.Application.ViewModels;
using QRMenu.Web.Models;
using System.Security.Claims;

namespace QRMenu.Web.Controllers
{
    // AccountController.cs
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService; // Ekledik

        public AccountController(
            IUserService userService,
            ILogService logService) // Constructor'a ekledik
        {
            _userService = userService;
            _logService = logService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.GetByEmailAsync(model.Email);
                if (result.IsSuccess)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, result.Data.Email),
                    new Claim(ClaimTypes.Role, result.Data.Role.ToString()),
                    new Claim("UserId", result.Data.Id.ToString())
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe
                        });

                    // Log kaydı ekledik
                    await _logService.LogInformationAsync(
                        module: "Account",
                        action: "Login",
                        details: $"Kullanıcı başarıyla giriş yaptı: {model.Email}",
                        userId: result.Data.Id);

                    return RedirectToAction("Index", "Home");
                }

                // Başarısız giriş logu
                await _logService.LogWarningAsync(
                    module: "Account",
                    action: "Login",
                    details: $"Başarısız giriş denemesi: {model.Email}");

                ModelState.AddModelError("", "Geçersiz email veya şifre");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Çıkış logu
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
                await _logService.LogInformationAsync(
                    module: "Account",
                    action: "Logout",
                    details: $"Kullanıcı çıkış yaptı: {User.FindFirst(ClaimTypes.Email)?.Value}",
                    userId: userId);
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}