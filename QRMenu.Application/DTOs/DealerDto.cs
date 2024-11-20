namespace QRMenu.Application.DTOs
{
    public class DealerDto : BaseDto
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string InstagramHandle { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
    }
}
