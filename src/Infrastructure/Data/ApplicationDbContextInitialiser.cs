using Delex_POS.Domain.Constants;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.ValueObjects;
using Delex_POS.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Delex_POS.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            // See https://jasontaylor.dev/ef-core-database-initialisation-strategies
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser 
        { 
            UserName = "administrator@localhost", 
            Email = "administrator@localhost"
        };

        administrator.Update(userCode: "000001",lastName: "Admin",firstName: "admin", middleName: null);

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        if (!_context.Accesses.Any()) 
        {
            _context.Accesses.Add(
                new AccessClaim(name: "Access Management", feature: "access", backendUrl: "", frontendUrl: "/access"));
            _context.Accesses.Add(
                new AccessClaim(name: "Account Management", feature: "accounts", backendUrl: "", frontendUrl: "/accounts"));
            _context.Accesses.Add(
                new AccessClaim(name: "User Management", feature: "users", backendUrl: "", frontendUrl: "/users"));
            _context.Accesses.Add(
                new AccessClaim(name: "Role Management", feature: "roles", backendUrl: "", frontendUrl: "/roles"));
            _context.Accesses.Add(
                new AccessClaim(name: "Product Management", feature: "products", backendUrl: "", frontendUrl: "/products"));
            _context.Accesses.Add(
                new AccessClaim(name: "Inventory Management", feature: "inventories", backendUrl: "", frontendUrl: "/inventory"));
            _context.Accesses.Add(
                new AccessClaim(name: "Point-Of-Sale", feature: "pos", backendUrl: "", frontendUrl: "/pos"));
            _context.Accesses.Add(
                new AccessClaim(name: "Reports Management", feature: "reports", backendUrl: "", frontendUrl: "/reports"));
            _context.Accesses.Add(
                new AccessClaim(name: "Warehouse Management", feature: "warehouses", backendUrl: "", frontendUrl: "/warehouses"));
            _context.Accesses.Add(
                new AccessClaim(name: "Branch Management", feature: "branches", backendUrl: "", frontendUrl: "/branches"));
            _context.Accesses.Add(
                new AccessClaim(name: "Cancel Reasons Management", feature: "cancel-reasons", backendUrl: "", frontendUrl: "/cancel-reasons"));
            _context.Accesses.Add(
                new AccessClaim(name: "Stock Transfer Management", feature: "stock-transfers", backendUrl: "", frontendUrl: "/stock-transfers"));
            await _context.SaveChangesAsync();
        }

        if (!_context.UserAccesses.Any(u => u.UserId == administrator.Id))
        {
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Account Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "User Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Role Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Product Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Inventory Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Point-Of-Sale").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Reports Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Warehouse Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Branch Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Cancel Reasons Management").Id, type: Domain.Enums.AccessType.Read));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Stock Transfer Management").Id, type: Domain.Enums.AccessType.Read));
            await _context.SaveChangesAsync();
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Tasks",
                Colour = Colour.Green,
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }


    }
}
