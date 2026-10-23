using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.AuditLog;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;
using Delex_POS.Application.Common.Interfaces.Repositories.Customer;
using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;
using Delex_POS.Application.Common.Interfaces.Repositories.Employee;
using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Application.Common.Interfaces.Repositories.Permission;
using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductType;
using Delex_POS.Application.Common.Interfaces.Repositories.Psgc;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
using Delex_POS.Application.Common.Interfaces.Repositories.RolePermission;
using Delex_POS.Application.Common.Interfaces.Repositories.Supplier;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
using Delex_POS.Application.Common.Interfaces.Repositories.UserPermission;
using Delex_POS.Application.Common.Interfaces.Repositories.UserHistory;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Infrastructure.Data;
using Delex_POS.Infrastructure.Data.Interceptors;
using Delex_POS.Infrastructure.Factories;
using Delex_POS.Infrastructure.Identity;
using Delex_POS.Infrastructure.Jwt;
using Delex_POS.Infrastructure.Mapping.IdentityProfile;
using Delex_POS.Infrastructure.Repositories.AccessClaim;
using Delex_POS.Infrastructure.Repositories.AuditLog;
using Delex_POS.Infrastructure.Repositories.Branch;
using Delex_POS.Infrastructure.Repositories.CancelReason;
using Delex_POS.Infrastructure.Repositories.Customer;
using Delex_POS.Infrastructure.Repositories.CustomerType;
using Delex_POS.Infrastructure.Repositories.Employee;
using Delex_POS.Infrastructure.Repositories.Generic;
using Delex_POS.Infrastructure.Repositories.PersonalInfo;
using Delex_POS.Infrastructure.Repositories.Permission;
using Delex_POS.Infrastructure.Repositories.Product;
using Delex_POS.Infrastructure.Repositories.ProductAttribute;
using Delex_POS.Infrastructure.Repositories.ProductCategory;
using Delex_POS.Infrastructure.Repositories.ProductClass;
using Delex_POS.Infrastructure.Repositories.ProductPurchasing;
using Delex_POS.Infrastructure.Repositories.ProductSale;
using Delex_POS.Infrastructure.Repositories.ProductType;
using Delex_POS.Infrastructure.Repositories.Psgc;
using Delex_POS.Infrastructure.Repositories.RoleAccess;
using Delex_POS.Infrastructure.Repositories.RolePermission;
using Delex_POS.Infrastructure.Repositories.Supplier;
using Delex_POS.Infrastructure.Repositories.UnitOfMeasure;
using Delex_POS.Infrastructure.Repositories.UserAccess;
using Delex_POS.Infrastructure.Repositories.UserPermission;
using Delex_POS.Infrastructure.Repositories.UserHistory;
using Delex_POS.Infrastructure.Repositories.Warehouse;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(cfg => {}, typeof(IdentityProfile));

        var connectionString = builder.Configuration.GetConnectionString(Services.DatabaseConnectionString);
        Guard.Against.Null(connectionString, message: $"Connection string '{Services.DatabaseConnectionString}' not found.");

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, SoftDeleteInterceptor>();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseMySQL(connectionString);
            options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        });

        // builder.EnrichMySqlDbContext<ApplicationDbContext>(configureSettings: settings =>
        // {
        //     settings.DisableRetry = false;
        //     settings.CommandTimeout = 30; // seconds
        // });

        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        //builder.Services.AddAuthorizationBuilder();

        builder.Services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders()            
            .AddApiEndpoints();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddScoped<TokenFactory>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();


        #region Repository
        builder.Services.AddScoped<IAccessClaimCommandRepository, AccessClaimCommandRepository>();
        builder.Services.AddScoped<IAccessClaimQueryRepository, AccessClaimQueryRepository>();
        builder.Services.AddScoped<IAuditLogCommandRepository, AuditLogCommandRepository>();
        builder.Services.AddScoped<IBranchCommandRepository, BranchCommandRepository>();
        builder.Services.AddScoped<IBranchQueryRepository, BranchQueryRepository>();
        builder.Services.AddScoped<ICancelReasonCommandRepository, CancelReasonCommandRepository>();
        builder.Services.AddScoped<ICancelReasonQueryRepository, CancelReasonQueryRepository>();
        builder.Services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
        builder.Services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();
        builder.Services.AddScoped<ICustomerTypeCommandRepository, CustomerTypeCommandRepository>();
        builder.Services.AddScoped<ICustomerTypeQueryRepository, CustomerTypeQueryRepository>();
        builder.Services.AddScoped<IEmployeeCommandRepository, EmployeeCommandRepository>();
        builder.Services.AddScoped<IEmployeeQueryRepository, EmployeeQueryRepository>();
        builder.Services.AddScoped<IGenericCommandRepository, GenericCommandRepository>();
        builder.Services.AddScoped<IGenericQueryRepository, GenericQueryRepository>();
        builder.Services.AddScoped<IPersonalInfoCommandRepository, PersonalInfoCommandRepository>();
        builder.Services.AddScoped<IPermissionQueryRepository, PermissionQueryRepository>();
        builder.Services.AddScoped<IPermissionCommandRepository, PermissionCommandRepository>();
        builder.Services.AddScoped<IPersonalInfoQueryRepository, PersonalInfoQueryRepository>();
        builder.Services.AddScoped<IProductCommandRepository, ProductCommandRepository>();
        builder.Services.AddScoped<IProductQueryRepository, ProductQueryRepository>();
        builder.Services.AddScoped<IProductAttributeCommandRepository, ProductAttributeCommandRepository>();
        builder.Services.AddScoped<IProductAttributeQueryRepository, ProductAttributeQueryRepository>();
        builder.Services.AddScoped<IProductCategoryCommandRepository, ProductCategoryCommandRepository>();
        builder.Services.AddScoped<IProductCategoryQueryRepository, ProductCategoryQueryRepository>();
        builder.Services.AddScoped<IProductClassCommandRepository, ProductClassCommandRepository>();
        builder.Services.AddScoped<IProductClassQueryRepository, ProductClassQueryRepository>();
        builder.Services.AddScoped<IProductPurchasingCommandRepository, ProductPurchasingCommandRepository>();
        builder.Services.AddScoped<IProductPurchasingQueryRepository, ProductPurchasingQueryRepository>();
        builder.Services.AddScoped<IProductSaleCommandRepository, ProductSaleCommandRepository>();
        builder.Services.AddScoped<IProductSaleQueryRepository, ProductSaleQueryRepository>();
        builder.Services.AddScoped<IProductTypeCommandRepository, ProductTypeCommandRepository>();
        builder.Services.AddScoped<IProductTypeQueryRepository, ProductTypeQueryRepository>();
        builder.Services.AddScoped<IPsgcRegionQueryRepository, PsgcRegionQueryRepository>();
        builder.Services.AddScoped<IPsgcProvinceQueryRepository, PsgcProvinceQueryRepository>();
        builder.Services.AddScoped<IPsgcMunicipalityQueryRepository, PsgcMunicipalityQueryRepository>();
        builder.Services.AddScoped<IPsgcBarangayQueryRepository, PsgcBarangayQueryRepository>();
        builder.Services.AddScoped<IRoleAccessCommandRepository, RoleAccessCommandRepository>();
        builder.Services.AddScoped<IRoleAccessQueryRepository, RoleAccessQueryRepository>();
        builder.Services.AddScoped<IRolePermissionCommandRepository, RolePermissionCommandRepository>();
        builder.Services.AddScoped<IRolePermissionQueryRepository, RolePermissionQueryRepository>();
        builder.Services.AddScoped<ISupplierCommandRepository, SupplierCommandRepository>();
        builder.Services.AddScoped<ISupplierQueryRepository, SupplierQueryRepository>();
        builder.Services.AddScoped<IUnitOfMeasureCommandRepository, UnitOfMeasureCommandRepository>();
        builder.Services.AddScoped<IUnitOfMeasureQueryRepository, UnitOfMeasureQueryRepository>();
        builder.Services.AddScoped<IUserAccessCommandRepository, UserAccessCommandRepository>();
        builder.Services.AddScoped<IUserAccessQueryRepository, UserAccessQueryRepository>();
        builder.Services.AddScoped<IUserPermissionCommandRepository, UserPermissionCommandRepository>();
        builder.Services.AddScoped<IUserPermissionQueryRepository, UserPermissionQueryRepository>();
        builder.Services.AddScoped<IUserHistoryCommandRepository, UserHistoryCommandRepository>();
        builder.Services.AddScoped<IUserHistoryQueryRepository, UserHistoryQueryRepository>();
        builder.Services.AddScoped<IWarehouseCommandRepository, WarehouseCommandRepository>();
        builder.Services.AddScoped<IWarehouseQueryRepository, WarehouseQueryRepository>();
        #endregion

    }
}
