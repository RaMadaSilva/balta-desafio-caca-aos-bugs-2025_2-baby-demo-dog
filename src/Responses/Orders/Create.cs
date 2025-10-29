namespace BugStore.Responses.Orders;

public record CreateOrderResponse
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public List<OrderLineResponse> Lines { get; init; } = new();
}

public record OrderLineResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string ProductTitle { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal Total { get; init; }
}