namespace Order.Domain.DTOs
{
    public sealed record AddressCreateDto(string AddressLine, string City, string Country, int CityCode);
    public sealed record AddressUpdateDto(string? AddressLine, string? City, string? Country, int? CityCode);
}
