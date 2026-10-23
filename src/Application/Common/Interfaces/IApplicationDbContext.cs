using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Enums;

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
    DbSet<Inventory> Inventories { get; }
    DbSet<InventoryTransaction> InventoryTransactions { get; }
    DbSet<Permission> Permissions { get; } 
    DbSet<PersonalInfo> PersonalInfos { get; }
    DbSet<Prescription> Prescriptions { get; }
    DbSet<PrescriptionItem> PrescriptionItems { get; }
    DbSet<PriceList> PriceLists { get; }
    DbSet<PriceListHistory> PriceListHistories { get; }
    DbSet<PriceListImplementation> PriceListImplementations { get; }
    DbSet<PriceListItem> PriceListItems { get; }
    DbSet<PriceListType> PriceListTypes { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductAttribute> ProductAttributes { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    DbSet<ProductClass> ProductClasses { get; }
    DbSet<ProductPurchasing> ProductPurchasings { get; }
    DbSet<ProductSale> ProductSales { get; }
    DbSet<ProductType> ProductTypes { get; }
    DbSet<PsgcRegion> Regions { get; }
    DbSet<PsgcProvince> Provinces { get; }
    DbSet<PsgcMunicipality> Municipalities { get; }
    DbSet<PsgcBarangay> Barangays { get; }
    DbSet<PurchaseOrder> PurchaseOrders { get; }
    DbSet<PurchaseOrderHistory> PurchaseOrderHistories { get; }
    DbSet<PurchaseOrderItem> PurchaseOrderItems { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<ReturnToSeller> ReturnToSellers { get; }
    DbSet<ReturnToSellerHistory> ReturnToSellerHistories { get; }
    DbSet<ReturnToSellerItem> ReturnToSellerItems { get; }
    DbSet<RoleAccess> RoleAccesses { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<Sale> Sales { get; }
    DbSet<SaleItem> SaleItems { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<TodoList> TodoLists { get; }
    DbSet<TransferApprovedItem> TransferApprovedItems { get; }
    DbSet<TransferRequest> TransferRequests { get; }
    DbSet<TransferRequestHistory> TransferRequestHistories { get; }
    DbSet<TransferRequestItem> TransferRequestItems { get; }    
    DbSet<UnitOfMeasure> UnitOfMeasures { get; }
    DbSet<UserAccess> UserAccesses { get; }
    DbSet<UserHistory> UserHistories { get; }
    DbSet<UserPermission> UserPermissions { get; }
    DbSet<Warehouse> Warehouses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
