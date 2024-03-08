using Shared.Core.Common;
using System.Text.Json.Serialization;

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

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("addressLine")]
        public string AddressLine { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("cityCode")]
        public int CityCode { get; set; }
    }
}
