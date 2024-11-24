// QRMenu.Web/Controllers/AccountController.cs
using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.ViewModels;
 
namespace QRMenu.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı doğrulama işlemleri burada yapılacak
                // Başarılı ise:
                // return RedirectToAction("Index", "Home");
                // Başarısız ise:
                // ModelState.AddModelError("", "Geçersiz giriş denemesi.");
            }
            return View(model);
        }
    }
}
