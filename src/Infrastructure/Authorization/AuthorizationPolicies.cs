//using Microsoft.AspNetCore.Authorization;
using Delex_POS.Infrastructure.Identity;
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


            // Specific Permission/Claims policy - BRANCH
            .AddPolicy("BranchCreate", policy =>
                policy.RequirePermission("branches:create"))

            .AddPolicy("BranchUpdate", policy =>
                policy.RequirePermission("branches:Update"))

            .AddPolicy("BranchDelete", policy =>
                policy.RequirePermission("branches:Delete"))

            .AddPolicy("BranchRead", policy =>
                policy.RequirePermission("branches:read"))

            // Specific Permission/Claims policy - CANCEL REASON
            .AddPolicy("CancelReasonCreate", policy =>
                policy.RequirePermission("cancel-reasons:create"))

            .AddPolicy("CancelReasonUpdate", policy =>
                policy.RequirePermission("cancel-reasons:update"))

            .AddPolicy("CancelReasonDelete", policy =>
                policy.RequirePermission("cancel-reasons:delete"))

            .AddPolicy("CancelReasonRead", policy =>
                policy.RequirePermission("cancel-reasons:read"))

            // Specific Permission/Claims policy - CUSTOMER
            .AddPolicy("CustomerCreate", policy =>
                policy.RequirePermission("customers:create"))

            .AddPolicy("CustomerUpdate", policy =>
                policy.RequirePermission("customers:update"))

            .AddPolicy("CustomerDelete", policy =>
                policy.RequirePermission("customers:delete"))

            .AddPolicy("CustomerRead", policy =>
                policy.RequirePermission("customers:read"))

            // Specific Permission/Claims policy - CUSTOMER TYPE
            .AddPolicy("CustomerTypeCreate", policy =>
                policy.RequirePermission("customer-types:create"))

            .AddPolicy("CustomerTypeUpdate", policy =>
                policy.RequirePermission("customer-types:update"))

            .AddPolicy("CustomerTypeDelete", policy =>
                policy.RequirePermission("customer-types:delete"))

            .AddPolicy("CustomerTypeRead", policy =>
                policy.RequirePermission("customer-types:read"))

            // Specific Permission/Claims policy - GENERIC
            .AddPolicy("GenericCreate", policy =>
                policy.RequirePermission("generics:create"))

            .AddPolicy("GenericUpdate", policy =>
                policy.RequirePermission("generics:update"))

            .AddPolicy("GenericDelete", policy =>
                policy.RequirePermission("generics:delete"))

            .AddPolicy("GenericRead", policy =>
                policy.RequirePermission("generics:read"))

            // Specific Permission/Claims policy - PRODUCT
            .AddPolicy("ProductCreate", policy =>
                policy.RequirePermission("products:create"))

            .AddPolicy("ProductUpdate", policy =>
                policy.RequirePermission("products:update"))

            .AddPolicy("ProductDelete", policy =>
                policy.RequirePermission("products:delete"))

            .AddPolicy("ProductRead", policy =>
                policy.RequirePermission("products:read"))

            // Specific Permission/Claims policy - ROLE
            .AddPolicy("RoleCreate", policy =>
                policy.RequirePermission("roles:create"))

            .AddPolicy("RoleUpdate", policy =>
                policy.RequirePermission("roles:update"))

            .AddPolicy("RoleDelete", policy =>
                policy.RequirePermission("roles:delete"))

            .AddPolicy("RoleRead", policy =>
                policy.RequirePermission("roles:read"))

            // Specific Permission/Claims policy - SUPPLIER
            .AddPolicy("SupplierCreate", policy =>
                policy.RequirePermission("suppliers:create"))

            .AddPolicy("SupplierUpdate", policy =>
                policy.RequirePermission("suppliers:update"))

            .AddPolicy("SupplierDelete", policy =>
                policy.RequirePermission("suppliers:delete"))

            .AddPolicy("SupplierRead", policy =>
                policy.RequirePermission("suppliers:read"))

            // Specific Permission/Claims policy - UNIT OF MEASURE
            .AddPolicy("UnitOfMeasureCreate", policy =>
                policy.RequirePermission("units-of-measure:create"))

            .AddPolicy("UnitOfMeasureUpdate", policy =>
                policy.RequirePermission("units-of-measure:update"))

            .AddPolicy("UnitOfMeasureDelete", policy =>
                policy.RequirePermission("unit-of-measure:delete"))

            .AddPolicy("UnitOfMeasureRead", policy =>
                policy.RequirePermission("unit-of-measure:read"))

            // Specific Permission/Claims policy - EMPLOYEE / USER
            .AddPolicy("UserCreate", policy =>
                policy.RequirePermission("users:create"))

            .AddPolicy("UserUpdate", policy =>
                policy.RequirePermission("user:update"))

            .AddPolicy("UserDelete", policy =>
                policy.RequirePermission("users:delete"))

            .AddPolicy("UserRead", policy =>
                policy.RequirePermission("users:read"))

            // Specific Permission/Claims policy - WAREHOUSE
            .AddPolicy("WarehouseCreate", policy =>
                policy.RequirePermission("warehouses:create"))

            .AddPolicy("WarehouseUpdate", policy =>
                policy.RequirePermission("warehouses:update"))

            .AddPolicy("WarehouseDelete", policy =>
                policy.RequirePermission("warehouses:delete"))

            .AddPolicy("WarehouseRead", policy =>
                policy.RequirePermission("warehouses:read"))

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
