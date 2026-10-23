using Microsoft.EntityFrameworkCore;
using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Delex_POS.Infrastructure.Data.Configurations;

public class UserHistoryConfiguration : IEntityTypeConfiguration<UserHistory>
{
    public void Configure(EntityTypeBuilder<UserHistory> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.EntryBy)
            .IsRequired();

        builder.Property(x => x.Remarks)
            .IsRequired();

    }
}
