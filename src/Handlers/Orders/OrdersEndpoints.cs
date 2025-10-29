using BugStore.Requests.Orders;

namespace BugStore.Handlers.Orders; 

public static class OrdersEndpoints
{
    public static void MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/orders").WithTags("Orders");

        // GET /v1/orders/{id} - Buscar por ID
        group.MapGet("/{id}", async (Guid id, Handler handler) =>
        {
            var request = new GetOrderByIdRequest { Id = id };
            var result = await handler.GetByIdAsync(request);
            
            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        // POST /v1/orders - Criar novo
        group.MapPost("/", async (
            CreateOrderRequest request,
            Handler handler) =>
        {
            var result = await handler.CreateAsync(request);
            
            if (result == null)
                return Results.BadRequest("Customer ou Produto não encontrado");
                
            return Results.Created($"/v1/orders/{result.Id}", result);
        });
    }
}
