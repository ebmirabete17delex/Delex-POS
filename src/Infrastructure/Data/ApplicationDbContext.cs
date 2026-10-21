using System.Reflection;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Domain.Entities;
using Delex_POS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;

namespace Delex_POS.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // RBAC Entities
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<AccessClaim> Accesses => Set<AccessClaim>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<CancelReason> CancelReasons => Set<CancelReason>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerType> CustomerTypes => Set<CustomerType>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Generic> Generics => Set<Generic>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<PersonalInfo> PersonalInfos => Set<PersonalInfo>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<ProductClass> ProductClasses => Set<ProductClass>();
    public DbSet<ProductPurchasing> ProductPurchasings => Set<ProductPurchasing>();
    public DbSet<ProductSale> ProductSales => Set<ProductSale>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<PsgcRegion> Regions => Set<PsgcRegion>();
    public DbSet<PsgcProvince> Provinces => Set<PsgcProvince>();
    public DbSet<PsgcMunicipality> Municipalities => Set<PsgcMunicipality>();
    public DbSet<PsgcBarangay> Barangays => Set<PsgcBarangay>();
    public DbSet<RoleAccess> RoleAccesses => Set<RoleAccess>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<UnitOfMeasure> UnitOfMeasures => Set<UnitOfMeasure>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<UserAccess> UserAccesses => Set<UserAccess>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
