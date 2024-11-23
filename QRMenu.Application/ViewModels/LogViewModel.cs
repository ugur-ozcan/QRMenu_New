 
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
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public int? DealerId { get; set; }
        public int? CompanyId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        // Kullanıcının rolü (örn. Admin, User)
        public string UserRole { get; set; } // Eksik olan alan eklendi
    }
}
