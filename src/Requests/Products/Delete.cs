namespace BugStore.Requests.Products;

public record DeleteProductRequest
{
    public required Guid Id { get; init; }
}