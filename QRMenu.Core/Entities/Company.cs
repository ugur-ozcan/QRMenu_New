using QRMenu.Core.Entities;
using QRMenu.Core.ValueObjects;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public int DealerId { get; set; }
    public string Logo { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
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


    public Location Location { get; private set; }
    private readonly List<BusinessHours> _businessHours = new();
    public IReadOnlyCollection<BusinessHours> BusinessHours => _businessHours.AsReadOnly();

    public void SetLocation(Location location)
    {
        Location = location;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddBusinessHours(BusinessHours businessHours)
    {
        _businessHours.Add(businessHours);
    }
}

