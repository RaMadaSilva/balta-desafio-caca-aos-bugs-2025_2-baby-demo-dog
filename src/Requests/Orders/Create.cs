namespace BugStore.Requests.Orders;

public record CreateOrderRequest
{
    public required Guid CustomerId { get; init; }
    public required List<OrderLineRequest> Lines { get; init; }
}

public record OrderLineRequest
{
    public required Guid ProductId { get; init; }
    public required int Quantity { get; init; }
}