using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(x => x.BranchId)
            .IsRequired();

        builder.Property(x => x.TotalSales)
            .IsRequired();

        builder.Property(x => x.TotalDiscount)
            .IsRequired();

        builder.Property(x => x.Cash)
            .IsRequired();

        builder.Property(x => x.Card)
            .IsRequired();

    }
}
