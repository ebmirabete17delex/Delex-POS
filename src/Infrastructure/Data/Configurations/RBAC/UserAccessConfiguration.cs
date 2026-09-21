using Delex_POS.Domain.Entities.RBAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class UserAccessConfiguration : IEntityTypeConfiguration<UserAccess>
{
    public void Configure(EntityTypeBuilder<UserAccess> builder)
    {
        builder.Property(u => u.UserId)
            .IsRequired();

        builder.Property(u => u.AccessId)
            .IsRequired();
    }
}
