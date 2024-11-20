using QRMenu.Core.Enums;
 

namespace QRMenu.Core.Entities
{
    public class NotificationTemplate : BaseEntity
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public NotificationType Type { get; set; }
        public string Parameters { get; set; } // JSON formatında template parametreleri
    }
}
