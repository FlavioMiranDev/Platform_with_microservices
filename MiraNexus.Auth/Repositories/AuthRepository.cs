using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using MiraNexus.Auth.Data.DTOs;
using MiraNexus.Auth.Models;

namespace MiraNexus.Auth.Repositories;

public class AuthRepository
{
    private readonly IDistributedCache _redisCache;

    public AuthRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<UserCacheDTO> GetAsync(string token)
    {
        string json = await _redisCache.GetStringAsync(token);

        if (String.IsNullOrEmpty(json)) return null;

        return JsonSerializer.Deserialize<UserCacheDTO>(json);
    }

    public async Task<UserCacheDTO> SaveAsync(UserCacheDTO user, string token)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(8)
        };
        await _redisCache.SetStringAsync(token, JsonSerializer.Serialize(user), options);

        return await GetAsync(token);
    }

    public async Task SetLoged(string email)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(8)
        };

        await _redisCache.SetStringAsync(email, "loged", options);
    }

    public async Task<bool> Exists(string email)
    {
        return !(string.IsNullOrEmpty(await _redisCache.GetStringAsync(email)));
    }

    public async Task RemoveAsync(string key)
    {
        await _redisCache.RemoveAsync(key);
    }
}
