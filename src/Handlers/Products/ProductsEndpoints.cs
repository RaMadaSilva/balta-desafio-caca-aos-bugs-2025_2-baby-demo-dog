using BugStore.Requests.Products;
using BugStore.Responses.Products;

namespace BugStore.Handlers.Products
{
    public static class ProductsEndpoints
    {
        public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/products").WithTags("Products");

            // GET /v1/products - Listar todos com busca e paginação
            group.MapGet("/", async (
                string? search,
                Handler handler,
                int page = 1,
                int pageSize = 10) =>
            {
                var request = new GetProductsRequest
                {
                    Search = search,
                    Page = page,
                    PageSize = pageSize
                };
                return Results.Ok(await handler.GetAllAsync(request));
            });

            // GET /v1/products/{id} - Buscar por ID
            group.MapGet("/{id}", async (Guid id, Handler handler) =>
            {
                var request = new GetProductByIdRequest { Id = id };
                var result = await handler.GetByIdAsync(request);
                
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            // POST /v1/products - Criar novo
            group.MapPost("/", async (
                CreateProductRequest request,
                Handler handler) =>
            {
                var result = await handler.CreateAsync(request);
                return Results.Created($"/v1/products/{result.Id}", result);
            });

            // PUT /v1/products/{id} - Atualizar
            group.MapPut("/{id}", async (
                Guid id,
                UpdateProductRequest request,
                Handler handler) =>
            {
                var result = await handler.UpdateAsync(id, request);
                
                return result is null ? Results.NotFound() : Results.Ok(result);
            });

            // DELETE /v1/products/{id} - Deletar
            group.MapDelete("/{id}", async (Guid id, Handler handler) =>
            {
                var request = new DeleteProductRequest { Id = id };
                var result = await handler.DeleteAsync(request);
                
                return result is null ? Results.NotFound() : Results.Ok(result);
            });
        }
    }
}
