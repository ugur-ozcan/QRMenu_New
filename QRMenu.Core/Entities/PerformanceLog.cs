
namespace QRMenu.Core.Entities
{
    public class PerformanceLog : BaseEntity
    {
        public string Endpoint { get; set; }
        public double ResponseTime { get; set; } // Milisaniye cinsinden
        public string RequestMethod { get; set; } // GET, POST, etc.
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public int? UserId { get; set; }
        public int? StatusCode { get; set; }
        public string QueryParameters { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }

}
