using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class CancelReasonConfiguration : IEntityTypeConfiguration<CancelReason>
{
    public void Configure(EntityTypeBuilder<CancelReason> builder)
    {
        builder.Property(cr => cr.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(cr => cr.IsActive)
            .HasDefaultValue(true);

        builder.Property(cr => cr.IsAllowUserToEnterMemo)
            .HasDefaultValue(true);

        builder.Property(cr => cr.Memo)
            .HasMaxLength(100);

    }
}
