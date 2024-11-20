using QRMenu.Core.Enums;
 

namespace QRMenu.Core.Entities
{
    // Bildirim sistemi için entityler
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public string Data { get; set; } // JSON formatında ek veriler
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public int? UserId { get; set; }
        public int? DealerId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public string Status { get; set; } // Pending, Sent, Failed
        public string ErrorMessage { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Dealer Dealer { get; set; }
        public virtual Company Company { get; set; }
    }
}
