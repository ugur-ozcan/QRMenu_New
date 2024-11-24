using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.DTOs;
using QRMenu.Application.Interfaces;
using QRMenu.Core.Enums;

namespace QRMenu.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IUserService userService, ILogger<AdminController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetAllAsync(1, 100, true, false);
            if (result.IsSuccess)
            {
                var admins = result.Data.Items.Where(u => u.Role == UserRole.Admin);
                return View(admins);
            }
            return View(new List<UserDto>());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserDto model, string password, string confirmPassword)
        {
            if (ModelState.IsValid)
            {
                if (password != confirmPassword)
                {
                    ModelState.AddModelError("", "Şifreler eşleşmiyor.");
                    return View(model);
                }

                model.Role = UserRole.Admin;
                model.IsActive = true;
                model.CreatedAt = DateTime.UtcNow;
                model.LastLoginIp = HttpContext.Connection.RemoteIpAddress.ToString();

                var result = await _userService.CreateAsync(model, password);
                if (result.IsSuccess)
                {
                    _logger.LogInformation($"Yeni admin eklendi: {model.Email}");
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message);
            }

            return View(model);
        }
    }
}
