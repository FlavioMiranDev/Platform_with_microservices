using Microsoft.EntityFrameworkCore;
using MiraNexus.Products.Data;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Repositories;

public class ProductRepository(AppDbContext context) : Repository<Product>(context)
{
    public async Task<IEnumerable<Product>> GetAllAsync(Guid id)
    {
        return await _context.Products.Where(p => p.TenantId == id).ToListAsync();
    }
}
