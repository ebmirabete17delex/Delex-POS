using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Infrastructure.Data;
using Delex_POS.Infrastructure.Data.Interceptors;
using Delex_POS.Infrastructure.Identity;
using Delex_POS.Infrastructure.Factories;
using Delex_POS.Infrastructure.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Delex_POS.Application.Common.Interfaces.Repositories.AccessClaim;
using Delex_POS.Infrastructure.Repositories.AccessClaim;
using Delex_POS.Application.Common.Interfaces.Repositories.Branch;
using Delex_POS.Infrastructure.Repositories.Branch;
using Delex_POS.Application.Common.Interfaces.Repositories.UserAccess;
using Delex_POS.Infrastructure.Repositories.UserAccess;
using Delex_POS.Application.Common.Interfaces.Repositories.RoleAccess;
using Delex_POS.Infrastructure.Repositories.RoleAccess;
using Delex_POS.Infrastructure.Mapping.IdentityProfile;

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
            builder.Services.AddScoped<IBranchCommandRepository, BranchCommandRepository>();
            builder.Services.AddScoped<IBranchQueryRepository, BranchQueryRepository>();
            builder.Services.AddScoped<IUserAccessCommandRepository, UserAccessCommandRepository>();
            builder.Services.AddScoped<IUserAccessQueryRepository, UserAccessQueryRepository>();
            builder.Services.AddScoped<IRoleAccessCommandRepository, RoleAccessCommandRepository>();
            builder.Services.AddScoped<IRoleAccessQueryRepository, RoleAccessQueryRepository>();
        #endregion

    }
}
