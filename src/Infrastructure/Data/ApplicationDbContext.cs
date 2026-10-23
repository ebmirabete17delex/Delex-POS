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

    public DbSet<AccessClaim> Accesses => Set<AccessClaim>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<CancelReason> CancelReasons => Set<CancelReason>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerType> CustomerTypes => Set<CustomerType>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Generic> Generics => Set<Generic>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<PersonalInfo> PersonalInfos => Set<PersonalInfo>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionItem> PrescriptionItems => Set<PrescriptionItem>();
    public DbSet<PriceList> PriceLists => Set<PriceList>();
    public DbSet<PriceListHistory> PriceListHistories => Set<PriceListHistory>();
    public DbSet<PriceListImplementation> PriceListImplementations => Set<PriceListImplementation>();
    public DbSet<PriceListItem> PriceListItems => Set<PriceListItem>();
    public DbSet<PriceListType> PriceListTypes => Set<PriceListType>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<ProductClass> ProductClasses => Set<ProductClass>();
    public DbSet<ProductPurchasing> ProductPurchasings => Set<ProductPurchasing>();
    public DbSet<ProductSale> ProductSales => Set<ProductSale>();
    public DbSet<ProductType> ProductTypes => Set<ProductType>();
    public DbSet<PsgcRegion> Regions => Set<PsgcRegion>();
    public DbSet<PsgcProvince> Provinces => Set<PsgcProvince>();
    public DbSet<PsgcMunicipality> Municipalities => Set<PsgcMunicipality>();
    public DbSet<PsgcBarangay> Barangays => Set<PsgcBarangay>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderHistory> PurchaseOrderHistories => Set<PurchaseOrderHistory>();
    public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<ReturnToSeller> ReturnToSellers => Set<ReturnToSeller>();
    public DbSet<ReturnToSellerHistory> ReturnToSellerHistories => Set<ReturnToSellerHistory>();
    public DbSet<ReturnToSellerItem> ReturnToSellerItems => Set<ReturnToSellerItem>();
    public DbSet<RoleAccess> RoleAccesses => Set<RoleAccess>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TransferApprovedItem> TransferApprovedItems => Set<TransferApprovedItem>();
    public DbSet<TransferRequest> TransferRequests => Set<TransferRequest>();
    public DbSet<TransferRequestHistory> TransferRequestHistories => Set<TransferRequestHistory>();
    public DbSet<TransferRequestItem> TransferRequestItems => Set<TransferRequestItem>();
    public DbSet<UnitOfMeasure> UnitOfMeasures => Set<UnitOfMeasure>();
    public DbSet<UserAccess> UserAccesses => Set<UserAccess>();
    public DbSet<UserHistory> UserHistories => Set<UserHistory>();
    public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
