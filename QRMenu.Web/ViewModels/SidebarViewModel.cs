namespace QRMenu.Web.ViewModels
{
    public class SidebarViewModel
    {
        public List<MenuItem> MenuItems { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string UserAvatar { get; set; }
    }

    public class MenuItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public List<MenuItem> Children { get; set; }

    }
}
