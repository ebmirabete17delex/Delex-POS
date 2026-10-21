//using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Delex_POS.Infrastructure.Authorization;

/// <summary>
/// Defines authorization policies for role-based access control (RBAC).
/// These policies can be used with [Authorize(Policy = "PolicyName")] attribute.
/// </summary>
public static class AuthorizationPolicies
{
    public const string Administrator = "Administrator";
    public const string Manager = "Manager";
    public const string Staff = "Staff";
    public const string User = "User";

    // Composite policies
    public const string AdministratorOrManager = "AdministratorOrManager";
    public const string ManagerOrStaff = "ManagerOrStaff";
    public const string AnyAuthenticated = "AnyAuthenticated";

    /// <summary>
    /// Registers authorization policies for the application.
    /// Call this in the DependencyInjection setup.
    /// </summary>
    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            // Single-role policies
            .AddPolicy(Administrator, policy =>
                policy.RequireRole(Domain.Constants.Roles.Administrator))

            .AddPolicy(Manager, policy =>
                policy.RequireRole("Manager"))

            .AddPolicy(Staff, policy =>
                policy.RequireRole("Staff"))

            .AddPolicy(User, policy =>
                policy.RequireRole("User"))

            // Composite policies (multiple roles allowed)
            .AddPolicy(AdministratorOrManager, policy =>
                policy.RequireRole(
                    Domain.Constants.Roles.Administrator,
                    "Manager"))

            .AddPolicy(ManagerOrStaff, policy =>
                policy.RequireRole("Manager", "Staff"))

            // Any authenticated user (no specific role required)
            .AddPolicy(AnyAuthenticated, policy =>
                policy.RequireAuthenticatedUser());
    }
}
