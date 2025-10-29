namespace BugStore.Requests.Customers;

public record GetCustomersRequest
{
    // Parâmetros de busca opcionais
    public string? Search { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}