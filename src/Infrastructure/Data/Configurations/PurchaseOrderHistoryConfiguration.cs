using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class PurchaseOrderHistoryConfiguration : IEntityTypeConfiguration<PurchaseOrderHistory>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderHistory> builder)
    {
        builder.HasQueryFilter(ac => !ac.IsDeleted);

        builder.Property(ac => ac.PurchaseOrderId)
            .IsRequired();

        builder.Property(ac => ac.Status)
            .IsRequired();

        builder.Property(ac => ac.Date)
            .IsRequired();

        builder.Property(ac => ac.UserId)
            .IsRequired();

    }
}
