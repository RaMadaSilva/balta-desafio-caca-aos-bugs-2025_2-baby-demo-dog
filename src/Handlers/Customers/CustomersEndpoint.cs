namespace BugStore.Handlers.Customers; 

public static class CustomersEndpoint
{
    public static void MapCustomersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/customers").WithTags("Customers");

        group.MapGet("/", () => "Hello World!");
        group.MapGet("/{id}", () => "Hello World!");
        group.MapPost("/", () => "Hello World!");
        group.MapPut("/{id}", () => "Hello World!");
        group.MapDelete("/{id}", () => "Hello World!");
    }
}
