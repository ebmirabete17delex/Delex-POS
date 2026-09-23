
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Delex_POS.Application.Common.Interfaces;

namespace Delex_POS.Infrastructure.Identity;

public class EndpointRoleAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public EndpointRoleAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IIdentityService identityService)
    {
        var path = context.Request.Path.Value?.ToLower() ?? string.Empty;

        // Example: Map path patterns to required roles dynamically or via config
        var requiredRole = GetRequiredRoleForPath(path);

        if (!string.IsNullOrEmpty(requiredRole))
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var isInRole = await identityService.IsInRoleAsync(userId, requiredRole);
            if (!isInRole)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }

        await _next(context);
    }

    private string? GetRequiredRoleForPath(string path)
    {
        // Replace with a database call or cached lookup table
        if (path.StartsWith("/api/admin")) return "Administrator";
        if (path.StartsWith("/api/reports")) return "Manager";

        return null; // No role restriction for this URL
    }
}