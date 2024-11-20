 
namespace QRMenu.Application.ViewModels
{
    public class LogViewModel
    {
        public DateTime CreatedAt { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public string LogLevel { get; set; }
        public string UserEmail { get; set; }
        public string IpAddress { get; set; }
    }
}
