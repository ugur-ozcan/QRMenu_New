using Microsoft.AspNetCore.Mvc;

namespace QRMenu.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Bu, `Views/Home/Index.cshtml` dosyasını yükler.
        }
    }
}
