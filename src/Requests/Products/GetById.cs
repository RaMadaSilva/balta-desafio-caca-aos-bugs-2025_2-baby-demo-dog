namespace BugStore.Requests.Products;

public record GetProductByIdRequest
{
    public required Guid Id { get; init; }
}