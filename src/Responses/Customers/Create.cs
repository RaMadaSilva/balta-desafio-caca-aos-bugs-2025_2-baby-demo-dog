namespace BugStore.Responses.Customers;

public record CreateCustomerResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public DateTime BirthDate { get; init; }
}