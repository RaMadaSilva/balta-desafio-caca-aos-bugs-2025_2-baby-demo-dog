namespace BugStore.Requests.Customers;

public record GetCustomerByIdRequest
{
    public required Guid Id { get; init; }
}