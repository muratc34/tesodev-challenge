using Shared.Core.Common;

namespace Customer.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        public Customer()
        {
        }
        public Customer(string name, string email, Guid addressId)
        {
            Name = name;
            Email = email;
            AddressId = addressId;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
