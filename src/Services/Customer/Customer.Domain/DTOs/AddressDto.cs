namespace Customer.Domain.DTOs
{
    public sealed record AddressCreateDto(string AddressLine, string City, string Country, int CityCode);
    public sealed record AddressDetailDto(Guid Id, string AddressLine, string City, string Country, int CityCode);
}
