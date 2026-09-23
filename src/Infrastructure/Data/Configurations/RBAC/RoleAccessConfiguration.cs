using Delex_POS.Domain.Entities.RBAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<RoleAccess>
{
    public void Configure(EntityTypeBuilder<RoleAccess> builder)
    {
        builder.Property(ur => ur.AccessId)
            .IsRequired();

        builder.Property(ur => ur.RoleId)
            .IsRequired();
    }
}
