using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<AccessClaim> Accesses { get; }
    DbSet<AuditLog> AuditLogs { get; }
    DbSet<Branch> Branches { get; }
    DbSet<CancelReason> CancelReasons { get; }
    DbSet<Customer> Customers { get; }
    DbSet<CustomerType> CustomerTypes { get; }
    DbSet<Employee> Employees { get; }
    DbSet<Generic> Generics { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<PersonalInfo> PersonalInfos { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductAttribute> ProductAttributes { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    DbSet<ProductClass> ProductClasses { get; }
    DbSet<ProductPurchasing> ProductPurchasings { get; }
    DbSet<ProductSale> ProductSales { get; }
    DbSet<PsgcRegion> Regions { get; }
    DbSet<PsgcProvince> Provinces { get; }
    DbSet<PsgcMunicipality> Municipalities { get; }
    DbSet<PsgcBarangay> Barangays { get; }
    DbSet<RoleAccess> RoleAccesses { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<TodoList> TodoLists { get; }
    DbSet<UnitOfMeasure> UnitOfMeasures { get; }
    DbSet<UserAccess> UserAccesses { get; }
    DbSet<UserPermission> UserPermissions { get; }
    DbSet<Warehouse> Warehouses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
