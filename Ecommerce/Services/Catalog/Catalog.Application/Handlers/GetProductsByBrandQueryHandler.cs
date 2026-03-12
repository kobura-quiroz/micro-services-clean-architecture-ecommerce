using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsByBrandQueryHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _repository;

    public GetProductsByBrandQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await _repository.GetProductsByBrand(request.BrandName);
        return productList.ToResponseList();
    }
}
