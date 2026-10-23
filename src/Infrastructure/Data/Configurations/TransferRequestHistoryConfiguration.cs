using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class TransferRequestHistoryConfiguration : IEntityTypeConfiguration<TransferRequestHistory>
{
    public void Configure(EntityTypeBuilder<TransferRequestHistory> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(x => x.TransferRequestId)
                .IsRequired();

        builder.Property(x => x.Status)
                .IsRequired();

        builder.Property(x => x.Date)
                .IsRequired();

        builder.Property(x => x.UserId)
                .IsRequired();

        builder.Property(x => x.CancelReasonId)
                .IsRequired();

        builder.Property(x => x.Remarks)
                .IsRequired();


    }
}
