using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetAllTypesQueryHandler : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
{
    private readonly ITypeRepository _repository;

    public GetAllTypesQueryHandler(ITypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typesList = await _repository.GetAllTypes();
        return typesList.ToResponseList();
    }
}
