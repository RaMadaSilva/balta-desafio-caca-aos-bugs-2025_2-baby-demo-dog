namespace BugStore.Responses.Orders;

public record GetOrderByIdResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public CustomerInfo Customer { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public List<OrderLineResponse> Lines { get; init; } = new();
    public decimal Total { get; init; }
}

public record CustomerInfo
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}

public record OrderLineDetailResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public ProductInfo Product { get; init; } = null!;
    public int Quantity { get; init; }
    public decimal Total { get; init; }
}

public record ProductInfo
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public decimal Price { get; init; }
}