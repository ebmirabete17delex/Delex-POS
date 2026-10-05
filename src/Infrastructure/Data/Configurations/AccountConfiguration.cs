using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(a => a.AccountId)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.AltKey)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.IsActive)
            .HasDefaultValue(true);

        builder.Property(a => a.BranchId)
            .IsRequired();

        builder.Property(a => a.IsRestrictToAssignedBranchOnly)
            .HasDefaultValue(false);

        builder.Property(a => a.AccountType)
            .IsRequired();

        builder.Property(a => a.IsAllowToLogon)
            .HasDefaultValue(false);

        builder.Property(a => a.Password)
            .HasMaxLength(100);

        builder.Property(a => a.AccessLevel)
            .IsRequired()
            .HasMaxLength(50);

    }
}
