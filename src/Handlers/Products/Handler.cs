using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products;

public class Handler
{
    private readonly AppDbContext _context;

    public Handler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CreateProductResponse> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Slug = request.Slug,
            Price = request.Price
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return new CreateProductResponse
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Slug = product.Slug,
            Price = product.Price
        };
    }

    public async Task<GetProductsResponse> GetAllAsync(GetProductsRequest request)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(p => p.Title.Contains(request.Search) || 
                                     p.Description.Contains(request.Search) ||
                                     p.Slug.Contains(request.Search));
        }

        var totalCount = await query.CountAsync();
        var products = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var productResponses = products.Select(p => new ProductResponse
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Slug = p.Slug,
            Price = p.Price
        });

        return new GetProductsResponse
        {
            Products = productResponses,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

    public async Task<GetProductByIdResponse?> GetByIdAsync(GetProductByIdRequest request)
    {
        var product = await _context.Products.FindAsync(request.Id);

        if (product == null)
            return null;

        return new GetProductByIdResponse
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Slug = product.Slug,
            Price = product.Price
        };
    }

    public async Task<UpdateProductResponse?> UpdateAsync(Guid id, UpdateProductRequest request)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return null;

        if (request.Title != null)
            product.Title = request.Title;

        if (request.Description != null)
            product.Description = request.Description;

        if (request.Slug != null)
            product.Slug = request.Slug;

        if (request.Price.HasValue)
            product.Price = request.Price.Value;

        await _context.SaveChangesAsync();

        return new UpdateProductResponse
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Slug = product.Slug,
            Price = product.Price
        };
    }

    public async Task<DeleteProductResponse?> DeleteAsync(DeleteProductRequest request)
    {
        var product = await _context.Products.FindAsync(request.Id);

        if (product == null)
            return null;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return new DeleteProductResponse
        {
            Id = request.Id,
            Success = true
        };
    }
}

