using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasQueryFilter(s => !s.IsDeleted);

        builder.HasIndex(s => s.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(s => s.SupplierId)
            .HasMaxLength(20);

        builder.Property(s => s.PersonalInfoId)
            .IsRequired();

        builder.Property(s => s.IsActive)
            .HasDefaultValue(true);

        builder.Property(s => s.BranchId)
            .IsRequired();

    }
}
