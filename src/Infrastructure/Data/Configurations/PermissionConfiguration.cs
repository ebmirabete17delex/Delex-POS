using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Domain.Entities.Permission>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Permission> builder)
    {
        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasIndex(p => p.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(p => p.AccessId)
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
