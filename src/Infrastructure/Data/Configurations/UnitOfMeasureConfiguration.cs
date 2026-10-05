using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

internal class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        builder.Property(u => u.UnitOfMeasureId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.UnitBase)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.IsGroupAsSingleQuantity)
            .HasDefaultValue(false);

        builder.Property(u => u.IsAsForQtyWhenSold)
            .HasDefaultValue(false);

        builder.Property(u => u.DecimalPlaces)
            .IsRequired();

    }
}
