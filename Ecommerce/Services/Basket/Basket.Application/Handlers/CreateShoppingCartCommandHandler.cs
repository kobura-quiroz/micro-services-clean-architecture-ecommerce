using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Responses;
using Basket.Core.Repositories;
using MediatR;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppigCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _repository;
    private readonly DiscountGrpcService _discountGrpcService;

    public CreateShoppingCartCommandHandler(IBasketRepository repository, DiscountGrpcService discountGrpcService)
    {
        _repository = repository;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppigCartCommand request, CancellationToken cancellationToken)
    {
        // Apply discounts using Grpc
        foreach (var item in request.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price -= coupon.Amount;
        }
        // Convert command to domain entity
        var shoppingCartEntity = BasketMapper.MapToEntity(request);

        // Save to redis
        var updatedCart = await _repository.UpsertBasket(shoppingCartEntity);

        // Convert back to response
        return BasketMapper.MapToResponse(updatedCart);
    }
}
