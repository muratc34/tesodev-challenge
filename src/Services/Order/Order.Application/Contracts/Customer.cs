using Order.Domain.Entities;
using Shared.Core.Primitives;
using System.Text.Json.Serialization;

namespace Order.Application.Contracts
{
    public class ResponseCustomerData
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("error")]
        public Error Error{ get; set; }

        [JsonPropertyName("data")]
        public Customer Data { get; set; }
    }
    public class Customer 
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }
    }
}
