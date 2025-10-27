namespace BugStore.Handlers.Orders; 

public static class OrdersEndpoints
{
    public static void MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/orders").WithTags("Orders");

        group.MapGet("/{id}", () => "Hello World!");
        group.MapPost("/", () => "Hello World!");
    }
}
