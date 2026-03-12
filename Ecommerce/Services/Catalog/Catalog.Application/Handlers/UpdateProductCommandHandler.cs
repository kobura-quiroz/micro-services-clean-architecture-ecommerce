using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public record UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetProductById(request.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found");
        }

        // Fetch brand and type
        var brand = await _repository.GetBrandsByIdAsync(request.BrandId);
        var type = await _repository.GetTypesByIdAsync(request.TypeId);
        if (brand == null || type == null)
        {
            throw new ApplicationException("Invalid Brand or Type specified");
        }

        // Map
        var updatedProduct = request.ToUpdateEntity(existing, brand, type);

        // Save record
        return await _repository.UpdateProduct(updatedProduct);
    }
}
