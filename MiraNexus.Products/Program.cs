using Microsoft.EntityFrameworkCore;
using MiraNexus.Products.Application;
using MiraNexus.Products.Data;
using MiraNexus.Products.Repositories;
using MiraNexus.Products.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<TenantRepository>();

builder.Services.AddScoped<ProductAppService>();
builder.Services.AddScoped<CategoryAppService>();
builder.Services.AddScoped<TenantAppService>();

var app = builder.Build();

app.MapGrpcService<ProductsGrpcService>();
app.MapGrpcService<CategoriesGrpcService>();
app.MapGrpcService<TenantsGrpcService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
