using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class UserPermissionConfiguration : IEntityTypeConfiguration<Domain.Entities.UserPermission>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.UserPermission> builder)
    {
        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasIndex(p => p.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(p => p.UserId)
            .IsRequired();

        builder.Property(p => p.PermissionId)
            .IsRequired();
    }
}
