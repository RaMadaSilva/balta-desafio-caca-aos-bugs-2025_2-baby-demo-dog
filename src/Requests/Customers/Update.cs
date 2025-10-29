namespace BugStore.Requests.Customers;

public record UpdateCustomerRequest
{
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public DateTime? BirthDate { get; init; }
}