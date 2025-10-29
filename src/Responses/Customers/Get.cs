namespace BugStore.Responses.Customers;

public record GetCustomersResponse
{
    public IEnumerable<CustomerResponse> Customers { get; init; } = Array.Empty<CustomerResponse>();
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

public record CustomerResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public DateTime BirthDate { get; init; }
}