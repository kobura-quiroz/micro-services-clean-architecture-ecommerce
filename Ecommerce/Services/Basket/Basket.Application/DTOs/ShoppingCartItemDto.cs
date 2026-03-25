namespace Basket.Application.DTOs;

public record ShoppingCartItemDto(
    string ProductId,
    string ProductName,
    string ImageFile,
    decimal Price,
    int Quantity);
