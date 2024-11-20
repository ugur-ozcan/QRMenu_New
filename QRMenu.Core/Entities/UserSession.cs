
namespace QRMenu.Core.Entities
{
    public class UserSession : BaseEntity
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsRevoked { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}
