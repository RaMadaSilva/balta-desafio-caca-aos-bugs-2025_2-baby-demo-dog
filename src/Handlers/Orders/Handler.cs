using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Orders;

public class Handler
{
    private readonly AppDbContext _context;

    public Handler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CreateOrderResponse?> CreateAsync(CreateOrderRequest request)
    {
        // Verificar se o customer existe
        var customer = await _context.Customers.FindAsync(request.CustomerId);
        if (customer == null)
            return null;

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Lines = new()
        };

        foreach (var line in request.Lines)
        {
            // Verificar se o produto existe e buscar preço
            var product = await _context.Products.FindAsync(line.ProductId);
            if (product == null)
                return null; // Produto não encontrado

            var total = product.Price * line.Quantity;

            var orderLine = new OrderLine
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                Total = total
            };

            order.Lines.Add(orderLine);
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Buscar produtos para incluir no response
        var products = await _context.Products
            .Where(p => request.Lines.Select(l => l.ProductId).Contains(p.Id))
            .ToListAsync();

        var linesResponse = order.Lines.Select(line =>
        {
            var product = products.First(p => p.Id == line.ProductId);
            return new OrderLineResponse
            {
                Id = line.Id,
                ProductId = line.ProductId,
                ProductTitle = product.Title,
                Quantity = line.Quantity,
                Total = line.Total
            };
        }).ToList();

        return new CreateOrderResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            Lines = linesResponse
        };
    }

    public async Task<GetOrderByIdResponse?> GetByIdAsync(GetOrderByIdRequest request)
    {
        var order = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Lines)
                .ThenInclude(l => l.Product)
            .FirstOrDefaultAsync(o => o.Id == request.Id);

        if (order == null)
            return null;

        var total = order.Lines.Sum(l => l.Total);

        return new GetOrderByIdResponse
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            Customer = new CustomerInfo
            {
                Id = order.Customer.Id,
                Name = order.Customer.Name,
                Email = order.Customer.Email
            },
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            Lines = order.Lines.Select(line => new OrderLineResponse
            {
                Id = line.Id,
                ProductId = line.ProductId,
                ProductTitle = line.Product.Title,
                Quantity = line.Quantity,
                Total = line.Total
            }).ToList(),
            Total = total
        };
    }
}

