using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class PriceLevelConfiguration : IEntityTypeConfiguration<PriceLevel>
{
    public void Configure(EntityTypeBuilder<PriceLevel> builder)
    {
        builder.Property(pl => pl.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pl => pl.IsActive)
            .HasDefaultValue(true);

        builder.Property(pl => pl.Memo)
            .HasMaxLength(100);

    }
}
