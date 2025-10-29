using BugStore.Requests.Customers;
using BugStore.Responses.Customers;

namespace BugStore.Handlers.Customers; 

public static class CustomersEndpoint
{
    public static void MapCustomersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/v1/customers").WithTags("Customers");

        // GET /v1/customers - Listar todos com busca e paginação
        group.MapGet("/", async (
            string? search,
            Handler handler,
            int page = 1,
            int pageSize = 10) =>
        {
            var request = new GetCustomersRequest
            {
                Search = search,
                Page = page,
                PageSize = pageSize
            };
            return Results.Ok(await handler.GetAllAsync(request));
        });

        // GET /v1/customers/{id} - Buscar por ID
        group.MapGet("/{id}", async (Guid id, Handler handler) =>
        {
            var request = new GetCustomerByIdRequest { Id = id };
            var result = await handler.GetByIdAsync(request);
            
            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        // POST /v1/customers - Criar novo
        group.MapPost("/", async (
            CreateCustomerRequest request,
            Handler handler) =>
        {
            var result = await handler.CreateAsync(request);
            return Results.Created($"/v1/customers/{result.Id}", result);
        });

        // PUT /v1/customers/{id} - Atualizar
        group.MapPut("/{id}", async (
            Guid id,
            UpdateCustomerRequest request,
            Handler handler) =>
        {
            var result = await handler.UpdateAsync(id, request);
            
            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        // DELETE /v1/customers/{id} - Deletar
        group.MapDelete("/{id}", async (Guid id, Handler handler) =>
        {
            var request = new DeleteCustomerRequest { Id = id };
            var result = await handler.DeleteAsync(request);
            
            return result is null ? Results.NotFound() : Results.Ok(result);
        });
    }
}
