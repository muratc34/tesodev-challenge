namespace Order.Domain.DTOs
{
    public sealed record AddressCreateDto(string AddressLine, string City, string Country, int CityCode);
    public sealed record AddressUpdateDto(Guid AddressId,string AddressLine, string City, string Country, int CityCode);
}
