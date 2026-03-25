namespace Basket.Application.DTOs;

public record ShoppingCartDto(
    string UserName,
    List<ShoppingCartItemDto> Items,
    decimal TotalPrice);
