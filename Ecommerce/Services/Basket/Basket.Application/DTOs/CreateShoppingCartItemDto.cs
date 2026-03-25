namespace Basket.Application.DTOs;

public record CreateShoppingCartItemDto(
    string ProductId,
    string ProductName,
    string ImageFile,
    decimal Price,
    int Quantity);
