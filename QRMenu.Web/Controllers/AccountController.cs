using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.Interfaces;
using QRMenu.Application.ViewModels;
using QRMenu.Core.Interfaces;
using QRMenu.Web.Models;
using System.Security.Claims;

namespace QRMenu.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService; // Kullanıcı servisi
        private readonly ILogService _logService; // Loglama servisi
        private readonly ICurrentUserService _currentUserService; // Mevcut kullanıcı bilgileri
        private readonly IUnitOfWork _unitOfWork; // Veritabanı işlemleri için

        public AccountController(
            IUserService userService,
            ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            ILogService logService)
        {
            _userService = userService;
            _logService = logService;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: Login
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
                new Claim(ClaimTypes.Name, result.Data.FullName),
                new Claim(ClaimTypes.Email, result.Data.Email),
                new Claim("UserId", result.Data.Id.ToString())
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        new AuthenticationProperties { IsPersistent = model.RememberMe });

                    await _logService.LogInformationAsync("Account", "Login", $"Kullanıcı başarıyla giriş yaptı: {model.Email}", userId: result.Data.Id);

                    return RedirectToAction("Index", "Home");
                }

                // Başarısız giriş logu (Error olarak değiştirildi)
                await _logService.LogErrorAsync(
                    module: "Account",
                    action: "Login",
                    details: $"Başarısız giriş denemesi: {model.Email}");

                ModelState.AddModelError("", "Geçersiz email veya şifre");
            }
            return View(model);
        }


        // GET: Profile
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userId = _currentUserService.UserId;
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _unitOfWork.Users.GetByIdAsync(userId.Value);

            var profileViewModel = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(profileViewModel);
        }

        // POST: Profile
        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.Users.GetByIdAsync(_currentUserService.UserId.Value);
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                await _unitOfWork.SaveChangesAsync();

                TempData["SuccessMessage"] = "Profil başarıyla güncellendi.";
                return RedirectToAction("Profile");
            }

            return View(model);
        }

        // GET: Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _logService.LogInformationAsync("Account", "Logout", $"Kullanıcı çıkış yaptı: {_currentUserService.Email}");
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
