using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Delex_POS.Application.Common.Interfaces;

namespace Delex_POS.Infrastructure.Identity;

public class PermissionClaimsTransformation(IPermissionService permissionService)
    : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity?.IsAuthenticated != true)
        {
            return principal;
        }

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return principal;
        }

        // Fetch permissions from database, then cache
        // IMPORTANT: Cache these results to avoid DB hits on every request
        var permissions = await permissionService.GetUserPermissionsAsync(userId);

        var claimsIdentity = (ClaimsIdentity)principal.Identity;
        foreach (var permission in permissions)
        {
            claimsIdentity.AddClaim(new Claim(CustomClaimTypes.Permission, $"{permission.Feature.ToLower()}:{permission.Action.ToLower()}"));
        }

        return principal;
    }
}
