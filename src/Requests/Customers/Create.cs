namespace BugStore.Requests.Customers;

public record CreateCustomerRequest
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required DateTime BirthDate { get; init; }
}