using BugStore.Data;
using BugStore.Handlers.Customers;
using BugStore.Handlers.Orders;
using BugStore.Handlers.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlite(connectionString));

// Registrar Handlers
builder.Services.AddScoped<BugStore.Handlers.Customers.Handler>();
builder.Services.AddScoped<BugStore.Handlers.Products.Handler>();
builder.Services.AddScoped<BugStore.Handlers.Orders.Handler>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCustomersEndpoints();
app.MapProductsEndpoints();
app.MapOrdersEndpoints();

app.Run();
