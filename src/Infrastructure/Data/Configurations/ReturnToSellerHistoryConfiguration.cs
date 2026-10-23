using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class ReturnToSellerHistoryConfiguration : IEntityTypeConfiguration<ReturnToSellerHistory>
{
    public void Configure(EntityTypeBuilder<ReturnToSellerHistory> builder)
    {
        builder.HasQueryFilter(ac => !ac.IsDeleted);

        builder.Property(ac => ac.ReturnToSellerId)
            .IsRequired();

        builder.Property(ac => ac.Status)
            .IsRequired();

        builder.Property(ac => ac.Date)
            .IsRequired();

        builder.Property(ac => ac.UserId)
            .IsRequired();

    }
}
