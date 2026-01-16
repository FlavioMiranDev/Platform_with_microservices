using Microsoft.EntityFrameworkCore;
using MiraNexus.Products.Data;
using MiraNexus.Products.Models;

namespace MiraNexus.Products.Repositories;

public class Repository<T> where T : EntityBase
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;

        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
