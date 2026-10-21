using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<RoleAccess>
{
    public void Configure(EntityTypeBuilder<RoleAccess> builder)
    {
        builder.HasQueryFilter(ur => !ur.IsDeleted);

        builder.HasIndex(ur => ur.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(ur => ur.AccessId)
            .IsRequired();

        builder.Property(ur => ur.RoleId)
            .IsRequired();
    }
}
