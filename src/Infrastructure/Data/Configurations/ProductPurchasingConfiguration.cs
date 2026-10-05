using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class ProductPurchasingConfiguration : IEntityTypeConfiguration<ProductPurchasing>
{
    public void Configure(EntityTypeBuilder<ProductPurchasing> builder)
    {
        builder.Property(pp => pp.ProductId)
            .IsRequired();

        builder.Property(pp => pp.PurchasingUnitId)
            .IsRequired();

        builder.Property(pp => pp.PrimarySupplierId)
            .IsRequired();

        builder.Property(pp => pp.MaxQuantity)
            .IsRequired();

        builder.Property(pp => pp.ReorderQuantity)
            .IsRequired();

    }
}
