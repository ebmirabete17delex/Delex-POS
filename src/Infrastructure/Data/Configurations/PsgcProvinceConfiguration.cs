using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class PsgcProvinceConfiguration : IEntityTypeConfiguration<PsgcProvince>
{
    public void Configure(EntityTypeBuilder<PsgcProvince> builder)
    {
        //builder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        builder.HasQueryFilter(ac => !ac.IsDeleted);

        builder.HasIndex(ac => ac.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(ac => ac.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(ac => ac.AreaName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ac => ac.IslandRegion)
            .IsRequired();

        builder.Property(ac => ac.Version)
            .IsRequired();

        builder.Property(ac => ac.RegionCode)
            .IsRequired()
            .HasMaxLength(10);

    }
}
