using Microsoft.AspNetCore.Mvc;
using QRMenu.Web.ViewModels;

public class SidebarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var currentPath = HttpContext.Request.Path;
        var menuItems = new List<MenuItem>
        {
            new MenuItem
            {
                Title = "Dashboard",
                Icon = "fas fa-home",
                Url = "/",
                IsActive = currentPath == "/"
            },
            new MenuItem
            {
                Title = "Adminler",
                Icon = "fas fa-users-cog",
                Url = "/Admin",
                IsActive = currentPath.StartsWithSegments("/Admin")
            },
            new MenuItem
            {
                Title = "Bayiler",
                Icon = "fas fa-building",
                Url = "/dealers"
            },
            new MenuItem
            {
                Title = "Firmalar",
                Icon = "fas fa-store",
                Url = "/companies"
            },
            new MenuItem
            {
                Title = "Raporlar",
                Icon = "fas fa-chart-bar",
                Url = "/reports"
            },
            new MenuItem
            {
                Title = "Ayarlar",
                Icon = "fas fa-cog",
                Url = "/settings"
            }
        };

        var model = new SidebarViewModel
        {
            MenuItems = menuItems,
            UserName = "Test Kullanıcı",
            UserRole = "Admin",
            UserAvatar = "/images/default-avatar.png"
        };

        return View(new SidebarViewModel { MenuItems = menuItems });
    }
}
