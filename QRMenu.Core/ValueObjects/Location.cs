using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRMenu.Core.ValueObjects
{
    public class Location : IEquatable<Location>
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string FormattedAddress { get; private set; }
        public string City { get; private set; }
        public string District { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string BuildingNumber { get; private set; }
        public string Street { get; private set; }

        private Location() { } // EF Core için

        private Location(
            double latitude,
            double longitude,
            string formattedAddress,
            string city,
            string district,
            string country,
            string postalCode = null,
            string buildingNumber = null,
            string street = null)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90 degrees");

            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180 degrees");

            Latitude = latitude;
            Longitude = longitude;
            FormattedAddress = formattedAddress;
            City = city;
            District = district;
            Country = country;
            PostalCode = postalCode;
            BuildingNumber = buildingNumber;
            Street = street;
        }

        public static Location Create(
            double latitude,
            double longitude,
            string formattedAddress,
            string city,
            string district,
            string country,
            string postalCode = null,
            string buildingNumber = null,
            string street = null)
        {
            return new Location(latitude, longitude, formattedAddress, city, district, country, postalCode, buildingNumber, street);
        }

        public double CalculateDistanceTo(Location other)
        {
            var d1 = Latitude * (Math.PI / 180.0);
            var d2 = other.Latitude * (Math.PI / 180.0);
            var num1 = Longitude * (Math.PI / 180.0);
            var num2 = other.Longitude * (Math.PI / 180.0);
            var d3 = num2 - num1;
            var d4 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(d3 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d4), Math.Sqrt(1.0 - d4)));
        }

        public bool Equals(Location other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Latitude.Equals(other.Latitude) &&
                   Longitude.Equals(other.Longitude) &&
                   FormattedAddress == other.FormattedAddress;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Location)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Latitude, Longitude, FormattedAddress);
        }
    }

}
