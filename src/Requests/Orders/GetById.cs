namespace BugStore.Requests.Orders;

public record GetOrderByIdRequest
{
    public required Guid Id { get; init; }
}