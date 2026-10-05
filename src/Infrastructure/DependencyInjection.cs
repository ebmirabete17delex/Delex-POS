using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.Account;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.CancelReason;
using Delex_POS.Application.Common.Interfaces.Repositories.CustomerType;
using Delex_POS.Application.Common.Interfaces.Repositories.Generic;
using Delex_POS.Application.Common.Interfaces.Repositories.Nationality;
using Delex_POS.Application.Common.Interfaces.Repositories.PersonalInfo;
using Delex_POS.Application.Common.Interfaces.Repositories.PriceLevel;
using Delex_POS.Application.Common.Interfaces.Repositories.Product;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductAttribute;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductCategory;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductClass;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductPurchasing;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductSale;
using Delex_POS.Application.Common.Interfaces.Repositories.ProductType;
using Delex_POS.Application.Common.Interfaces.Repositories.Region;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
using Delex_POS.Application.Common.Interfaces.Repositories.UnitOfMeasure;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
using Delex_POS.Application.Common.Interfaces.Repositories.Warehouse;
using Delex_POS.Infrastructure.Data;
using Delex_POS.Infrastructure.Data.Interceptors;
using Delex_POS.Infrastructure.Factories;
using Delex_POS.Infrastructure.Identity;
using Delex_POS.Infrastructure.Mapping;
using Delex_POS.Infrastructure.Mapping.IdentityProfile;
using Delex_POS.Infrastructure.Repositories.AccessClaim;
using Delex_POS.Infrastructure.Repositories.Account;
using Delex_POS.Infrastructure.Repositories.Branch;
using Delex_POS.Infrastructure.Repositories.CancelReason;
using Delex_POS.Infrastructure.Repositories.CustomerType;
using Delex_POS.Infrastructure.Repositories.Generic;
using Delex_POS.Infrastructure.Repositories.Nationality;
using Delex_POS.Infrastructure.Repositories.PersonalInfo;
using Delex_POS.Infrastructure.Repositories.PriceLevel;
using Delex_POS.Infrastructure.Repositories.Product;
using Delex_POS.Infrastructure.Repositories.ProductAttribute;
using Delex_POS.Infrastructure.Repositories.ProductCategory;
using Delex_POS.Infrastructure.Repositories.ProductClass;
using Delex_POS.Infrastructure.Repositories.ProductPurchasing;
using Delex_POS.Infrastructure.Repositories.ProductSale;
using Delex_POS.Infrastructure.Repositories.ProductType;
using Delex_POS.Infrastructure.Repositories.Region;
using Delex_POS.Infrastructure.Repositories.RoleAccess;
using Delex_POS.Infrastructure.Repositories.UnitOfMeasure;
using Delex_POS.Infrastructure.Repositories.UserAccess;
using Delex_POS.Infrastructure.Repositories.Warehouse;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

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

        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

        builder.Services.AddAuthorizationBuilder();

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
        builder.Services.AddScoped<IAccountCommandRepository, AccountCommandRepository>();
        builder.Services.AddScoped<IAccountQueryRepository, AccountQueryRepository>();
        builder.Services.AddScoped<IBranchCommandRepository, BranchCommandRepository>();
        builder.Services.AddScoped<IBranchQueryRepository, BranchQueryRepository>();
        builder.Services.AddScoped<ICancelReasonCommandRepository, CancelReasonCommandRepository>();
        builder.Services.AddScoped<ICancelReasonQueryRepository, CancelReasonQueryRepository>();
        builder.Services.AddScoped<ICustomerTypeCommandRepository, CustomerTypeCommandRepository>();
        builder.Services.AddScoped<ICustomerTypeQueryRepository, CustomerTypeQueryRepository>();
        builder.Services.AddScoped<IGenericCommandRepository, GenericCommandRepository>();
        builder.Services.AddScoped<IGenericQueryRepository, GenericQueryRepository>();
        builder.Services.AddScoped<INationalityCommandRepository, NationalityCommandRepository>();
        builder.Services.AddScoped<INationalityQueryRepository, NationalityQueryRepository>();
        builder.Services.AddScoped<IPersonalInfoCommandRepository, PersonalInfoCommandRepository>();
        builder.Services.AddScoped<IPersonalInfoQueryRepository, PersonalInfoQueryRepository>();
        builder.Services.AddScoped<IPriceLevelCommandRepository, PriceLevelCommandRepository>();
        builder.Services.AddScoped<IPriceLevelQueryRepository, PriceLevelQueryRepository>();
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
        builder.Services.AddScoped<IRegionCommandRepository, RegionCommandRepository>();
        builder.Services.AddScoped<IRegionQueryRepository, RegionQueryRepository>();
        builder.Services.AddScoped<IRoleAccessCommandRepository, RoleAccessCommandRepository>();
        builder.Services.AddScoped<IRoleAccessQueryRepository, RoleAccessQueryRepository>();
        builder.Services.AddScoped<IUnitOfMeasureCommandRepository, UnitOfMeasureCommandRepository>();
        builder.Services.AddScoped<IUnitOfMeasureQueryRepository, UnitOfMeasureQueryRepository>();
        builder.Services.AddScoped<IUserAccessCommandRepository, UserAccessCommandRepository>();
        builder.Services.AddScoped<IUserAccessQueryRepository, UserAccessQueryRepository>();
        builder.Services.AddScoped<IWarehouseCommandRepository, WarehouseCommandRepository>();
        builder.Services.AddScoped<IWarehouseQueryRepository, WarehouseQueryRepository>();
        #endregion

    }
}
