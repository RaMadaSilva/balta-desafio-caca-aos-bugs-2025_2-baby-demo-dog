namespace BugStore.Handlers.Products
{
    public static class ProductsEndpoints
    {
        public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/v1/products").WithTags("Products");

            group.MapGet("/", () => "Hello World!");
            group.MapGet("/{id}", () => "Hello World!");
            group.MapPost("/", () => "Hello World!");
            group.MapPut("/{id}", () => "Hello World!");
            group.MapDelete("/{id}", () => "Hello World!");
        }
    }
}
