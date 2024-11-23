using QRMenu.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace QRMenu.Core.Entities
{
    public class Company : BaseEntity
    {
        private List<BusinessHours> _businessHours;

        public Company()
        {
            _businessHours = new List<BusinessHours>();
            Branches = new List<Branch>();
            Users = new List<User>();
        }

        public string Name { get; set; }
        public string Slug { get; set; }
        public int DealerId { get; set; }
        public string Logo { get; set; }
        public string PhoneNumber { get; set; }
        public string CategoryView { get; set; }
        public string ProductView { get; set; }
        public bool UseBranches { get; set; }
        public bool ShowTableNumbers { get; set; }
        public string LanguagesSupported { get; set; }
        public string DefaultLanguage { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public DateTime? LastSyncDate { get; set; }

        // Navigation properties
        public virtual Dealer Dealer { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual CompanyTheme CompanyTheme { get; set; }

        // Value Objects
        public Location Location { get; private set; }

        [JsonIgnore]
        public IReadOnlyCollection<BusinessHours> BusinessHours => _businessHours.AsReadOnly();

        public void SetLocation(Location location)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddBusinessHours(BusinessHours businessHours)
        {
            if (businessHours == null)
                throw new ArgumentNullException(nameof(businessHours));

            _businessHours.Add(businessHours);
            UpdatedAt = DateTime.UtcNow;
        }

        public void ClearBusinessHours()
        {
            _businessHours.Clear();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetBusinessHours(IEnumerable<BusinessHours> businessHours)
        {
            if (businessHours == null)
                throw new ArgumentNullException(nameof(businessHours));

            _businessHours = businessHours.ToList();
            UpdatedAt = DateTime.UtcNow;
        }
    }
}