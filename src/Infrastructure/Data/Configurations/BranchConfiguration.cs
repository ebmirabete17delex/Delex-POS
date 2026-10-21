using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.HasQueryFilter(b => !b.IsDeleted);

        builder.HasIndex(b => b.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.Property(b => b.WarehouseId)
            .IsRequired();

        builder.Property(b => b.Address2)
            .HasMaxLength(200);

        builder.Property(b => b.RegionCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.ProvinceCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.MunicipalityCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.BarangayCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.Phone)
            .HasMaxLength(15);

        builder.Property(b => b.Fax)
            .HasMaxLength(15);

        builder.Property(b => b.Email)
            .HasMaxLength(50);

        builder.Property(b => b.Tin)
            .HasMaxLength(12);

    }
}
