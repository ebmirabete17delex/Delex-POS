using Delex_POS.Domain.Entities.RBAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.Property(b => b.Code)
            .HasMaxLength(4)
            .IsRequired();

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.Location)
            .HasMaxLength(100);

        builder.Property(b => b.Email)
            .HasMaxLength(50);

        builder.Property(b => b.ContactNumber)
            .HasMaxLength(20);
    }
}
