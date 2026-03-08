namespace Catalog.Application.DTOs;

public record ProductDto(
    string Id,
    string Name,
    string Summary,
    string Description,
    string ImageFile,
    BrandDto Brand,
    TypeDto Type,
    decimal Price,
    DateTimeOffset CreatedOn
    );
