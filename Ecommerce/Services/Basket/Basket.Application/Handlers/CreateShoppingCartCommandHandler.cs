using Basket.Application.Commands;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppigCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _repository;

    public CreateShoppingCartCommandHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppigCartCommand request, CancellationToken cancellationToken)
    {
        // Convert command to domain entity
        var shoppingCartEntity = BasketMapper.MapToEntity(request);

        // Save to redis
        var updatedCart = await _repository.UpsertBasket(shoppingCartEntity);

        // Convert back to response
        return BasketMapper.MapToResponse(updatedCart);
    }
}
