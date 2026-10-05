using System.Reflection;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Domain.Entities;
using Delex_POS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // RBAC Entities
    public DbSet<AccessClaim> Accesses => Set<AccessClaim>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<CancelReason> CancelReasons => Set<CancelReason>();
    public DbSet<CustomerType> CustomerTypes => Set<CustomerType>();
    public DbSet<Generic> Generics => Set<Generic>();
    public DbSet<Nationality> Nationalities => Set<Nationality>();
    public DbSet<PersonalInfo> PersonalInfos => Set<PersonalInfo>();
    public DbSet<PriceLevel> PriceLevels => Set<PriceLevel>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<ProductClass> ProductClasses => Set<ProductClass>();
    public DbSet<ProductPurchasing> ProductPurchasings => Set<ProductPurchasing>();
    public DbSet<ProductSale> ProductSales => Set<ProductSale>();
    public DbSet<Region> Regions => Set<Region>();
    public DbSet<RoleAccess> RoleAccesses => Set<RoleAccess>();
    public DbSet<UnitOfMeasure> UnitOfMeasures => Set<UnitOfMeasure>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<UserAccess> UserAccesses => Set<UserAccess>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
