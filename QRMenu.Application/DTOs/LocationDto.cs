namespace QRMenu.Application.DTOs
{
    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FormattedAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string BuildingNumber { get; set; }
        public string Street { get; set; }
    }
}
