namespace BugStore.Requests.Products;

public record CreateProductRequest
{
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public required string Slug { get; init; }
    public required decimal Price { get; init; }
}