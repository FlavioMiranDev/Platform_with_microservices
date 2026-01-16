using Microsoft.EntityFrameworkCore;
using MiraNexus.Products.Data;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Repositories;

public class CategoryRepository(AppDbContext context) : Repository<Category>(context)
{
    public async Task<IEnumerable<Category>> GetAllAsync(Guid id)
    {
        return await _context.Categories.Where(p => p.TenantId == id).ToListAsync();
    }
}
