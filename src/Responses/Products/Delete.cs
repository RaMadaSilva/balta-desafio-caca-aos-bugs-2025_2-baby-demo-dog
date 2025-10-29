namespace BugStore.Responses.Products;

public record DeleteProductResponse
{
    public Guid Id { get; init; }
    public bool Success { get; init; }
}