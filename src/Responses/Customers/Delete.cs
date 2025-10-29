namespace BugStore.Responses.Customers;

public record DeleteCustomerResponse
{
    public Guid Id { get; init; }
    public bool Success { get; init; }
}