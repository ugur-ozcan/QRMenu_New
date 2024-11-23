// Proje: QRMenu.Web
// Klasör: Controllers
// Dosya: AccountController.cs

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.Interfaces;
using QRMenu.Web.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace QRMenu.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService; // Kullanıcı yönetimi
        private readonly ILogger<AccountController> _logger; // İşlem logları

        public AccountController(IUserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: /Account/Login - Kullanıcı giriş sayfasını render eder
        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogInformation("Login page accessed at {Time}", DateTime.UtcNow); // Login sayfası erişildiğinde log kaydı
            return View(); // Login sayfasını gösterir
        }

        // POST: /Account/Login - Kullanıcı girişi kontrol edilir
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            _logger.LogInformation("Login attempt by user: {Username} at {Time}", username, DateTime.UtcNow); // Giriş denemesi loglanır

            // Kullanıcı veritabanında arar
            var user = await _userService.GetUserByUsernameAsync(username); // Kullanıcı adı ile arama yapılır

            if (user != null && _userService.ValidatePassword(user, password)) // Kullanıcı şifresi doğrulanır
            {
                _logger.LogInformation("Successful login by user: {Username} at {Time}", username, DateTime.UtcNow); // Başarılı giriş loglanır
                return RedirectToAction("Index", "Dashboard"); // Dashboard'a yönlendirilir
            }
            else // Kullanıcı bulunamadı veya şifre yanlış
            {
                _logger.LogWarning("Failed login attempt by user: {Username} at {Time}", username, DateTime.UtcNow); // Hatalı giriş loglanır
                ModelState.AddModelError(string.Empty, "Invalid username or password"); // Hata mesajı gösterilir
                return View(); // Login sayfası yeniden render edilir
            }
        }

        // POST: /Account/Logout - Kullanıcı çıkışı yapılır
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _logger.LogInformation("User logged out at {Time}", DateTime.UtcNow); // Logout log kaydı
            return RedirectToAction("Login", "Account"); // Login sayfasına yönlendirilir
        }
    }
}
