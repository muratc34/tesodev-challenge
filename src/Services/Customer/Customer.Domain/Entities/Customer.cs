﻿using Shared.Core.Common;

namespace Customer.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
