using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Application.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToResponse(this Product product)
    {
        if (product == null) return null;
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Summary = product.Summary,
            Brand = product.Brand,
            CreatedOn = product.CreatedOn,
            Description = product.Description,
            ImageFile = product.ImageFile,
            Price = product.Price,
            Type = product.Type,
        };
    }

    public static Pagination<ProductResponse> ToResponse(this Pagination<Product> pagination)
    => new Pagination<ProductResponse>(
        pagination.PageIndex,
        pagination.PageSize,
        pagination.Count,
        pagination.Data.Select(p => p.ToResponse()).ToList());

    public static IList<ProductResponse> ToResponseList(this IEnumerable<Product> products) =>
        products.Select(p => p.ToResponse()).ToList();

    public static Product ToEntity(this CreateProductCommand command, ProductBrand brand, ProductType type) =>
        new Product
        {
            Name = command.Name,
            Summary = command.Summary,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Brand = brand,
            Type = type,
            Price = command.Price,
            CreatedOn = DateTimeOffset.UtcNow,
        };

    public static Product ToUpdateEntity(
        this UpdateProductCommand command,
        Product existing,
        ProductBrand brand,
        ProductType type) =>
        new Product
        {
            Id = existing.Id,
            Name = command.Name,
            Summary = command.Summary,
            ImageFile = command.ImageFile,
            Brand = brand,
            Type = type,
            Price = command.Price,
            CreatedOn = existing.CreatedOn,
        };
}
