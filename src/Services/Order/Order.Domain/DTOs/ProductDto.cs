namespace Order.Domain.DTOs
{
    public sealed record ProductCreateDto(string ImageUrl, string Name);
    public sealed record ProductUpdateDto(string? ImageUrl, string? Name);
}
