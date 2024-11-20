using QRMenu.Core.Enums;

namespace QRMenu.Core.Entities
{

    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public int? DealerId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }

        // Navigation properties
        public virtual Dealer Dealer { get; set; }
        public virtual Company Company { get; set; }
    }


}
