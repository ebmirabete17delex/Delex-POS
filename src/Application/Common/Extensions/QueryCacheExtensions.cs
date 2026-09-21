using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Delex_POS.Application.Common.Extensions;

public static class QueryCacheExtensions
{
    private static MemoryCache s_cache = new(new MemoryCacheOptions());

    private static string GetCacheKey(IQueryable query)
    {
        var queryString = query.ToQueryString();
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(queryString));
        return Convert.ToBase64String(hash);
    }

    private static string GetCacheByKey(string cacheKey)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(cacheKey));
        return Convert.ToBase64String(hash);
    }
    
    public static List<T> FromCache<T>(this IQueryable<T> query)
    {
        var key = GetCacheKey(query);
        
        var result =  s_cache.GetOrCreate(key, cache =>
        {
            cache.SlidingExpiration = TimeSpan.FromMinutes(10);
            return query.ToList();
        }) ?? [];
        
        return result;
    }
    
    public static async Task<List<T>> FromCacheAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
    {
        var key = GetCacheKey(query);
        
        if (s_cache.TryGetValue(key, out List<T>? value))
            return value!;

        var result =  await s_cache.GetOrCreateAsync(key, cache =>
        {
            cache.SlidingExpiration = TimeSpan.FromMinutes(10);
            return query.ToListAsync(cancellationToken);
        }) ?? [];

        return result;
    }
    
    public static async Task<int> FromCacheCountAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default)
    {
        var key = GetCacheKey(query);
        
        if (s_cache.TryGetValue(key, out int value))
            return value;

        var result =  await s_cache.GetOrCreateAsync(key, cache =>
        {
            cache.SlidingExpiration = TimeSpan.FromMinutes(10);
            return query.CountAsync(cancellationToken);
        });

        return result;
    }

    public static async Task<T?> FromCacheFirstAsync<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, string id,
        CancellationToken cancellationToken = default)
    {
        var key = GetCacheByKey(query.ToQueryString() + id);
        
        if (s_cache.TryGetValue(key, out T? value))
            return value;

        var result = await s_cache.GetOrCreateAsync(key, cache =>
        {
            cache.SlidingExpiration = TimeSpan.FromMinutes(10);
            return query.FirstOrDefaultAsync(predicate, cancellationToken);
        }) ?? default;

        return result;
    }
    
    public static T? GetFromCache<T>(T obj, string cacheKey)
    {
        var key = GetCacheByKey(cacheKey);

        var result = s_cache.GetOrCreate(key, cache =>
        {
            cache.SlidingExpiration = TimeSpan.FromMinutes(10);
            return obj;
        }) ?? default;

        return result;
    }

    public static void Clear()
    {
        s_cache.Dispose();
        s_cache = new MemoryCache(new MemoryCacheOptions());
    }
}
