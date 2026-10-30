using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Models;
using Microsoft.Extensions.Caching.Memory;
using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;

namespace Delex_POS.Infrastructure.Identity;

public class PermissionService : IPermissionService
{
    private readonly IMemoryCache _cache;
    private readonly IUserPermissionQueryRepository _userPermissionQueryRepository;
    private readonly IIdentityService _identityService;
    public PermissionService(
     IIdentityService identityService,
     IUserPermissionQueryRepository userPermissionQueryRepository,
     IMemoryCache cache)
    {
        _identityService = identityService;
        _userPermissionQueryRepository = userPermissionQueryRepository;
        _cache = cache;
    }
    public async Task<bool> UserHasPermissionAsync(string userId, string resource, string action)
    {
        // First check cache
        var cacheKey = $"permissions_{userId}";
        if (!_cache.TryGetValue(cacheKey, out List<PermissionDto>? permissions))
        {
            await GetPermissionsQuery(userId, cacheKey!);
        }
        if (permissions is null || permissions.Count    > 0)
            return false;
        else
            return true; 
    }
    public async Task<List<PermissionDto>> GetUserPermissionsAsync(string userId)
    {
        var cacheKey = $"permissions_{userId}";
        if (!_cache.TryGetValue(cacheKey, out List<PermissionDto>? permissions))
        {
            // Same logic as in UserHasPermissionAsync to get permissions
            // [Implementation same as above]
            permissions = await GetPermissionsQuery(userId, cacheKey!);
            // Cache the result
            // [Implementation same as above]
        }
        if (permissions is null || permissions.Count == 0)
        {
            permissions = await GetPermissionsQuery(userId, cacheKey!);
        }
        return permissions;
    }
    // Other method implementations…
    public async Task<List<PermissionDto>> GetPermissionsQuery(string userId, string cacheKey)
    {

        var userPermissions = await _userPermissionQueryRepository
            .GetGrantedPermissionByUserId(userId, new CancellationToken());

        if (userPermissions is not null && userPermissions.Any())
        {
            // Cache for 10 minutes
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
            _cache.Set(cacheKey, userPermissions.ToList, cacheEntryOptions);
        }
        return [..userPermissions!];
    }
}
