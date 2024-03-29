﻿namespace Customer.Domain.DTOs
{
    public sealed record CustomerCreateDto(string Name, string Email, AddressCreateDto Address);
    public sealed record CustomerUpdateDto(Guid CustomerId, string? Name, string? Email, AddressUpdateDto? Address);
    public sealed record CustomerDetailDto(Guid Id, DateTime CreatedAt, DateTime? UpdatedAt, string Name, string Email, AddressDetailDto Address);
    
}