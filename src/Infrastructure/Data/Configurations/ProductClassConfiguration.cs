using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

internal class ProductClassConfiguration : IEntityTypeConfiguration<ProductClass>
{
    public void Configure(EntityTypeBuilder<ProductClass> builder)
    {
        builder.Property(pc => pc.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pc => pc.ProductTypeId)
            .IsRequired();

        builder.Property(pc => pc.Memo)
            .HasMaxLength(100);

        builder.Property(pc => pc.TaxTypeId)
            .IsRequired();

        builder.Property(pc => pc.UnitOfMeasurementId)
            .IsRequired();

        builder.Property(pc => pc.GenericNameId)
            .IsRequired();

    }
}
