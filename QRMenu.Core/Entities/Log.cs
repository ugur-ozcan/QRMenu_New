using QRMenu.Core.Enums;

namespace QRMenu.Core.Entities
{
    public class Log : BaseEntity
    {
        public string Module { get; set; }
        public string Action { get; set; }
        public string Details { get; set; }
        public LogLevel LogLevel { get; set; }
        public string IpAddress { get; set; }
        public int? UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string Exception { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }

}
