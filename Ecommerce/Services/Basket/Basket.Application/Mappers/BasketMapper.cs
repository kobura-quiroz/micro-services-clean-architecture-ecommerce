using Basket.Application.Commands;
using Basket.Application.Responses;
using Basket.Core.Entities;

namespace Basket.Application.Mappers;

public static class BasketMapper
{
    public static ShoppingCartResponse ToResponse(this ShoppingCart shoppingCart) =>
        new ShoppingCartResponse
        {
            UserName = shoppingCart.UserName,
            Items = shoppingCart.Items.Select(item => new ShoppingCartItemResponse
            {
                Quantity = item.Quantity,
                ImageFile = item.ImageFile,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
            }).ToList()
        };

    // Delegate based mapper
    public static readonly Func<ShoppingCart, ShoppingCartResponse> MapToResponse = 
        cart => new ShoppingCartResponse
        {
            UserName = cart.UserName,
            Items = cart.Items.Select(item => new ShoppingCartItemResponse
            {
                Quantity = item.Quantity,
                ImageFile = item.ImageFile,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
            }).ToList()
        };

    public static readonly Func<CreateShoppigCartCommand, ShoppingCart> MapToEntity =
        command => new ShoppingCart
        {
            UserName = command.UserName,
            Items = command.Items.Select(item => new ShoppingCartItem
            {
                ImageFile = item.ImageFile,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity
            }).ToList()
        };
}
