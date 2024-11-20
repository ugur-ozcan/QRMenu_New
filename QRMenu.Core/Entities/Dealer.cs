namespace QRMenu.Core.Entities
{
    public class Dealer : BaseEntity
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string InstagramHandle { get; set; }
        public DateTime LicenseExpiryDate { get; set; }

        // Navigation properties
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }


}
