using Delex_POS.Domain.Entities;

namespace Delex_POS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<AccessClaim> Accesses { get; }
    DbSet<Account> Accounts { get; }
    DbSet<Branch> Branches { get; }
    DbSet<CancelReason> CancelReasons { get; }
    DbSet<CustomerType> CustomerTypes { get; }
    DbSet<Generic> Generics { get; }
    DbSet<Nationality> Nationalities { get; }
    DbSet<PersonalInfo> PersonalInfos { get; }
    DbSet<PriceLevel> PriceLevels { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductAttribute> ProductAttributes { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    DbSet<ProductClass> ProductClasses { get; }
    DbSet<ProductPurchasing> ProductPurchasings { get; }
    DbSet<ProductSale> ProductSales { get; }
    DbSet<Region> Regions { get; }
    DbSet<RoleAccess> RoleAccesses { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<TodoList> TodoLists { get; }
    DbSet<UnitOfMeasure> UnitOfMeasures { get; }
    DbSet<UserAccess> UserAccesses { get; }
    DbSet<Warehouse> Warehouses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
