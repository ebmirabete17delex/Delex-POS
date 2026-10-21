using System.Text.Json;
using Delex_POS.Application.Psgc.Commands;
using Delex_POS.Domain.Constants;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.ValueObjects;
using Delex_POS.Infrastructure.Identity;
using Google.Protobuf.Compiler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
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
        var cashierRole = new IdentityRole(Roles.Cashier);
        var pharmacistRole = new IdentityRole(Roles.Pharmacist);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
            await _roleManager.CreateAsync(cashierRole);
            await _roleManager.CreateAsync(pharmacistRole);
        }

        // Default users
        var administrator = new ApplicationUser 
        { 
            UserName = "administrator@localhost", 
            Email = "administrator@localhost",
            UserCode = "000001"
        };

        administrator.Activate();

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }

            var pi = new PersonalInfo(
                title: "", 
                firstName: "Admin", 
                lastName: "Admin", 
                middleName: "", 
                nameExt: "", 
                birthDate: DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                gender: Domain.Enums.Gender.Male, 
                civilStatus: Domain.Enums.CivilStatus.Married, 
                email: "administrator@localhost", 
                billingAddress1: "", 
                billingAddress2: "",
                regionCode: "",
                provinceCode: "",
                municipalityCode: "",
                barangayCode: "",
                phone: "", 
                mobile: "", 
                fax: "", 
                tin: "", 
                contactPerson: "");

            await _context.PersonalInfos.AddAsync(pi);
            var ee_id = await _context.Employees.AddAsync(new Employee(administrator.Id, pi.Id, 0, ""));
        }

        if (!_context.Accesses.Any()) 
        {
            _context.Accesses.Add(
                new AccessClaim(name: "Access Management", feature: "access"));
            _context.Accesses.Add(
                new AccessClaim(name: "Role Management", feature: "roles"));
            _context.Accesses.Add(
                new AccessClaim(name: "Branch Management", feature: "branches"));
            _context.Accesses.Add(
                new AccessClaim(name: "User Management", feature: "users"));
            _context.Accesses.Add(
                new AccessClaim(name: "Region Management", feature: "regions"));
            _context.Accesses.Add(
                new AccessClaim(name: "Warehouse Management", feature: "warehouses"));
            _context.Accesses.Add(
                new AccessClaim(name: "Customer Management", feature: "customers"));
            _context.Accesses.Add(
                new AccessClaim(name: "Supplier Management", feature: "suppliers"));
            _context.Accesses.Add(
                new AccessClaim(name: "Product Management", feature: "products"));
            _context.Accesses.Add(
                new AccessClaim(name: "Inventory Management", feature: "inventories"));
            _context.Accesses.Add(
                new AccessClaim(name: "Point-Of-Sale", feature: "pos"));
            _context.Accesses.Add(
                new AccessClaim(name: "Reports Management", feature: "reports"));
            _context.Accesses.Add(
                new AccessClaim(name: "Cancel Reasons Management", feature: "cancel-reasons"));
            _context.Accesses.Add(
                new AccessClaim(name: "Stock Transfer Management", feature: "stock-transfers"));
            await _context.SaveChangesAsync();
        }
        if (_context.Accesses.Any())
        {
            foreach (var access in _context.Accesses)
            {
                _context.Permissions.Add(new Permission(access.Id, "Read"));
                _context.Permissions.Add(new Permission(access.Id, "Create"));
                _context.Permissions.Add(new Permission(access.Id, "Delete"));
                _context.Permissions.Add(new Permission(access.Id, "Update"));
            }
        }
        if (!_context.UserAccesses.Any(u => u.UserId == administrator.Id))
        {
            _context.UserAccesses.Add(new UserAccess(userId: administrator.Id, accessId: _context.Accesses.First(a => a.Name == "Customer Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Supplier Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "User Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Role Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Product Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Inventory Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Point-Of-Sale").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Reports Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Warehouse Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Branch Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Cancel Reasons Management").Id));
            _context.UserAccesses.Add(new UserAccess ( userId : administrator.Id, accessId : _context.Accesses.First(a => a.Name == "Stock Transfer Management").Id));
            await _context.SaveChangesAsync();
        }

        if (!_context.Warehouses.Any())
        {
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH001", name: "Sauyo Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH002", name: "Makati Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH003", name: "Mandaluyong Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH004", name: "Laguna Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH005", name: "Batangas Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH006", name: "Pangasinan Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH007", name: "Santiago Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH008", name: "Lucena Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH009", name: "Camarines Sur Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
            _context.Warehouses.Add(new Warehouse(warehouseId: "WH010", name: "Cebu Warehouse", memo: "", address1: "", address2: "", regionCode: "", provinceCode: "", municipalityCode: "", barangayCode: "", phone: "", fax: "", email: "sauyowarehouse@delex.com"));
        }

        if (!_context.Regions.Any())
        {
            var psgcsJson = GetPSGCFromJsonFile("C:\\github\\Delex-POS\\postman\\collections\\PSGC\\.resources\\GetRegions.resources\\examples");
            foreach(var r in psgcsJson)
            {
                _context.Regions.Add(new PsgcRegion(r.Code, r.Area_Name, r.Island_Region, r.Version));
            }
        }
        if (!_context.Provinces.Any())
        {
            var psgcsJson = GetPSGCFromJsonFile("C:\\github\\Delex-POS\\postman\\collections\\PSGC\\.resources\\GetProvinces.resources\\examples");
            foreach (var r in psgcsJson)
            {
                _context.Provinces.Add(new PsgcProvince(r.Code, r.Area_Name, r.Island_Region, r.Version, r.Code.Substring(0, 2).PadRight(10, '0')));
            }
        }
        if (!_context.Municipalities.Any())
        {
            var psgcsJson = GetPSGCFromJsonFile("C:\\github\\Delex-POS\\postman\\collections\\PSGC\\.resources\\GetMunicipalities.resources\\examples");
            foreach (var r in psgcsJson)
            {
                _context.Municipalities.Add(new PsgcMunicipality(r.Code, r.Area_Name, r.Island_Region, r.Version, r.Code.Substring(0, 5).PadRight(10, '0')));
            }
        }
        if (!_context.Barangays.Any())
        {
            var psgcsJson = GetPSGCFromJsonFile("C:\\github\\Delex-POS\\postman\\collections\\PSGC\\.resources\\GetBarangays.resources\\examples");
            foreach (var r in psgcsJson)
            {
                _context.Barangays.Add(new PsgcBarangay(r.Code, r.Area_Name, r.Island_Region, r.Version, r.Code.Substring(0, 7).PadRight(10, '0')));
            }
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

    
    private List<PsgcResponseResult> GetPSGCFromJsonFile(string path)
    {
        var source = new PsgcResponse();
        var psgcs = new List<PsgcResponseResult>();

        var di = new DirectoryInfo(path);
        foreach (FileInfo fi in di.EnumerateFiles("*.json"))
        {
            using (StreamReader r = fi.OpenText())
            {
                string json = r.ReadToEnd();
                source = System.Text.Json.JsonSerializer.Deserialize<PsgcResponse>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
                psgcs.AddRange([.. source!.Results]);
            }
        }
        if (psgcs.Count > 0)
            return psgcs;
        else
            return [];
    }

    class PsgcResponse
    {
        public int Count { get; init; }
        public string Next { get; init; } = string.Empty;
        public string Previous { get; init; } = string.Empty;
        public IEnumerable<PsgcResponseResult> Results { get; set; } = [];
    }

    class PsgcResponseResult
    {
        public string Code { get; set; } = string.Empty;
        public string Area_Name { get; set; } = string.Empty;
        public string Island_Region { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }
}
