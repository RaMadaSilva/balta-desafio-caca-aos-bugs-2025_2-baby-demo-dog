namespace BugStore.Responses.Products;

public record GetProductsResponse
{
    public IEnumerable<ProductResponse> Products { get; init; } = Array.Empty<ProductResponse>();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

public record ProductResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public decimal Price { get; init; }
}