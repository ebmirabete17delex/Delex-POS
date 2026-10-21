using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class UserAccessConfiguration : IEntityTypeConfiguration<UserAccess>
{
    public void Configure(EntityTypeBuilder<UserAccess> builder)
    {
        builder.HasQueryFilter(u => !u.IsDeleted);

        builder.HasIndex(u => u.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(u => u.UserId)
            .IsRequired();

        builder.Property(u => u.AccessId)
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();
    }
}
