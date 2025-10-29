using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers;

public class Handler
{
    private readonly AppDbContext _context;

    public Handler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CreateCustomerResponse> CreateAsync(CreateCustomerRequest request)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            BirthDate = request.BirthDate
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return new CreateCustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            BirthDate = customer.BirthDate
        };
    }

    public async Task<GetCustomersResponse> GetAllAsync(GetCustomersRequest request)
    {
        var query = _context.Customers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(c => c.Name.Contains(request.Search) || 
                                     c.Email.Contains(request.Search));
        }

        var totalCount = await query.CountAsync();
        var customers = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        var customerResponses = customers.Select(c => new CustomerResponse
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone,
            BirthDate = c.BirthDate
        });

        return new GetCustomersResponse
        {
            Customers = customerResponses,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

    public async Task<GetCustomerByIdResponse?> GetByIdAsync(GetCustomerByIdRequest request)
    {
        var customer = await _context.Customers.FindAsync(request.Id);

        if (customer == null)
            return null;

        return new GetCustomerByIdResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            BirthDate = customer.BirthDate
        };
    }

    public async Task<UpdateCustomerResponse?> UpdateAsync(Guid id, UpdateCustomerRequest request)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
            return null;

        if (request.Name != null)
            customer.Name = request.Name;

        if (request.Email != null)
            customer.Email = request.Email;

        if (request.Phone != null)
            customer.Phone = request.Phone;

        if (request.BirthDate.HasValue)
            customer.BirthDate = request.BirthDate.Value;

        await _context.SaveChangesAsync();

        return new UpdateCustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            Phone = customer.Phone,
            BirthDate = customer.BirthDate
        };
    }

    public async Task<DeleteCustomerResponse?> DeleteAsync(DeleteCustomerRequest request)
    {
        var customer = await _context.Customers.FindAsync(request.Id);

        if (customer == null)
            return null;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return new DeleteCustomerResponse
        {
            Id = request.Id,
            Success = true
        };
    }
}

