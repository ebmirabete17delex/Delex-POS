using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
    {
        builder.HasQueryFilter(ac => !ac.IsDeleted);

        builder.Property(ac => ac.PurchaseOrderId)
            .IsRequired();

        builder.Property(ac => ac.ProductId)
            .IsRequired();

        builder.Property(ac => ac.Quantity)
            .IsRequired();

    }
}
