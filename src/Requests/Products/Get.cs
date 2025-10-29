namespace BugStore.Requests.Products;

public record GetProductsRequest
{
    public string? Search { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}