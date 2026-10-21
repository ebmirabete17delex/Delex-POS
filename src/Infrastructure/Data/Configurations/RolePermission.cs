using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class RolePermission : IEntityTypeConfiguration<Domain.Entities.RolePermission>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.RolePermission> builder)
    {
        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasIndex(p => p.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(p => p.RoleId)
            .IsRequired();

        builder.Property(p => p.PermissionId)
            .IsRequired();
    }
}
