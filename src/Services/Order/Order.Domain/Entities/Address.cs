using Shared.Core.Common;

namespace Order.Domain.Entities
{
    public sealed class Address : IBaseEntity
    {
        public Address()
        {
        }
        public Address(string addressLine, string city, string country, int cityCode)
        {
            AddressLine = addressLine;
            City = city;
            Country = country;
            CityCode = cityCode;
        }
        public Guid Id { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CityCode { get; set; }
    }
}
