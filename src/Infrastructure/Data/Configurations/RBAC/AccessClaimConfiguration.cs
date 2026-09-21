using Delex_POS.Domain.Entities.RBAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class AccessClaimConfiguration : IEntityTypeConfiguration<AccessClaim>
{
    public void Configure(EntityTypeBuilder<AccessClaim> builder)
    {
        builder.Property(ac => ac.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ac => ac.Feature)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ac => ac.BackendUrl)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ac => ac.FrontendUrl)
            .HasMaxLength(100)
            .IsRequired();

    }
}