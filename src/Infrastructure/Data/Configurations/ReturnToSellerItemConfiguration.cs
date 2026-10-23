using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class ReturnToSellerItemConfiguration : IEntityTypeConfiguration<ReturnToSellerItem>
{
    public void Configure(EntityTypeBuilder<ReturnToSellerItem> builder)
    {
        builder.HasQueryFilter(ac => !ac.IsDeleted);

        builder.Property(ac => ac.ReturnToSellerId)
            .IsRequired();

        builder.Property(ac => ac.ProductId)
            .IsRequired();

        builder.Property(ac => ac.Quantity)
            .IsRequired();

    }
}
