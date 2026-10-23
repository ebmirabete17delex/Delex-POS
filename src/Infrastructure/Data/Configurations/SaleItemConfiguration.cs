using Microsoft.EntityFrameworkCore;
using Delex_POS.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(x => x.SalesId)
                .IsRequired();

        builder.Property(x => x.ProductId)
                .IsRequired();

        builder.Property(x => x.Quantity)
                .IsRequired();

        builder.Property(x => x.TotalAmount)
                .IsRequired();

    }
}
