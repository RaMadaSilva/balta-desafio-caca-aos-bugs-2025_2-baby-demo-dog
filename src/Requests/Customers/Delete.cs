namespace BugStore.Requests.Customers;

public record DeleteCustomerRequest
{
    public required Guid Id { get; init; }
}