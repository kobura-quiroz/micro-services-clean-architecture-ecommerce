using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsByNameQueryHandler : IRequestHandler<GetProductsByNameQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _repository;

    public GetProductsByNameQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<ProductResponse>> Handle(GetProductsByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await _repository.GetProductsByName(request.Name);
        var productResponseList = productList.ToResponseList();
        return productResponseList;
    }
}
