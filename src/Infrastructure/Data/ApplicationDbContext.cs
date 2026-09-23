using System.Reflection;
using Delex_POS.Application.Common.Interfaces;
using Delex_POS.Domain.Entities;
using Delex_POS.Domain.Entities.RBAC;
using Delex_POS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Delex_POS.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // RBAC Entities
    public DbSet<Branch> Branches => Set<Branch>();
    
    public DbSet<RoleAccess> RoleAccesses => Set<RoleAccess>();
    
    public DbSet<AccessClaim> Accesses => Set<AccessClaim>();

    public DbSet<UserAccess> UserAccesses => Set<UserAccess>();

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
