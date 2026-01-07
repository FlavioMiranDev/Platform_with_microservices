using Microsoft.EntityFrameworkCore;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Data;

public class AppDbContext : DbContext
{
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions options) : base(options) { }
}
