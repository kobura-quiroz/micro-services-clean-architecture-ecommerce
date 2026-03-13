namespace Basket.Application.Responses;

public record class ShoppingCartResponse
{
    public string UserName { get; set; }
    public List<ShoppingCartItemResponse> Items { get; set; }

    public ShoppingCartResponse()
    {
        UserName = string.Empty;
        Items = new List<ShoppingCartItemResponse>();
    }

    public ShoppingCartResponse(string userName) : this(userName, new List<ShoppingCartItemResponse>())
    {
    }

    public ShoppingCartResponse(string userName, List<ShoppingCartItemResponse> items)
    {
        userName = userName ?? string.Empty;
        items = items ?? new List<ShoppingCartItemResponse>();
    }

    public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);
}
