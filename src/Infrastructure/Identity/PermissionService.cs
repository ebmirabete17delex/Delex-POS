using System.Security.AccessControl;
using AutoMapper;
using Delex_POS.Infrastructure.Data;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Models;
using Delex_POS.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;
using Microsoft.AspNetCore.Identity;
using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;

namespace Delex_POS.Infrastructure.Identity;

public class PermissionService : IPermissionService
{
    private readonly IMemoryCache _cache;
    private readonly IRolePermissionQueryRepository _rolePermissionQueryRepository;
    private readonly IUserPermissionQueryRepository _userPermissionQueryRepository;
    private readonly IIdentityService _identityService;
    public PermissionService(
     IIdentityService identityService,
     IUserPermissionQueryRepository userPermissionQueryRepository,
     IRolePermissionQueryRepository rolePermissionQueryRepository,
     IMemoryCache cache)
    {
        _identityService = identityService;
        _userPermissionQueryRepository = userPermissionQueryRepository;
        _rolePermissionQueryRepository = rolePermissionQueryRepository;
        _cache = cache;
    }
    public async Task<bool> UserHasPermissionAsync(string userId, string resource, string action)
    {
        // First check cache
        var cacheKey = $"permissions_{userId}";
        if (!_cache.TryGetValue(cacheKey, out List<PermissionDto>? permissions))
        {
            // Get user roles
            var userRoles = await _identityService.GetUserRolesAsync(userId);
            Guard.Against.NotFound(userId, userRoles);
            // Get all permissions for these roles
            foreach (var userRole in userRoles)
            {
                var rolePermissions = await _rolePermissionQueryRepository.GetRolePermissionsAsync(userRole.RoleId, new CancellationToken());

                // Map to PermissionDto
                permissions = rolePermissions.Select(rp => new PermissionDto
                {
                    Feature = rp.Feature,
                    Action = rp.Name
                }).ToList();
                // Cache for 10 minutes
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                _cache.Set(cacheKey, permissions, cacheEntryOptions);

            }
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

            // Cache the result
            // [Implementation same as above]
        }
        if (permissions is null) return [];
        return permissions;
    }
    // Other method implementations…
}
