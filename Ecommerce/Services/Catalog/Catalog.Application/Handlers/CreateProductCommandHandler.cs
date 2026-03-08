using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _repository;

    public CreateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Fetch brand and type from repository
        var brand = await _repository.GetBrandsByIdAsync(request.BrandId);
        var type = await _repository.GetTypesByIdAsync(request.TypeId);

        if (brand == null || type == null)
        {
            throw new ApplicationException("Invalid Brand or Type specified");
        }

        // Match to entity
        var productEntity = request.ToEntity(brand, type);
        var newProduct = await _repository.CreateProduct(productEntity);
        return newProduct.ToResponse();
    }
}
