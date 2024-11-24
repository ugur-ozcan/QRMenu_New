using Microsoft.AspNetCore.Mvc;
using QRMenu.Application.Interfaces;
using QRMenu.Web.ViewModels;

namespace QRMenu.Web.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ICurrentUserService _currentUserService;

        public NavbarViewComponent(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new NavbarViewModel
            {
                UserName = _currentUserService.UserName,
                UserAvatar = "/images/default-avatar.png",
                UnreadNotificationsCount = 3 // Örnek değer
            };

            return View(model);
        }
    }
}