using Microsoft.EntityFrameworkCore;
using MiraNexus.Auth.Data;
using MiraNexus.Auth.Models;
using MiraNexus.Auth.Utils;

namespace MiraNexus.Auth.Repositories;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await GetByEmailAsync(email) is not null;
    }

    public async Task<User> CreateAsync(User user)
    {
        user.ValidationCode = GenerateCode.Generate();

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return user;
    }
}
