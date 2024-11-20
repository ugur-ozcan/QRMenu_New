

namespace QRMenu.Core.Entities
{
    // Güvenlik için entityler
    public class BlockedIp : BaseEntity
    {
        public string IpAddress { get; set; }
        public string Reason { get; set; }
        public DateTime BlockedUntil { get; set; }
        public int FailedAttempts { get; set; }
        public string LastUserAgent { get; set; }
    }
}
