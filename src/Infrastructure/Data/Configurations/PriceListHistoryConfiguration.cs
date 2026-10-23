using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class PriceListHistoryConfiguration : IEntityTypeConfiguration<PriceListHistory>
{
    public void Configure(EntityTypeBuilder<PriceListHistory> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(x => x.PriceListId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.StatusDate)
            .IsRequired();

        builder.Property(x => x.StatusBy)
            .IsRequired();

        builder.Property(x => x.Remarks)
            .IsRequired();

    }
}
