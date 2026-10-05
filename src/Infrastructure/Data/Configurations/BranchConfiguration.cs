using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {

        builder.Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(b => b.IsActive)
            .HasDefaultValue(true);

        builder.Property(b => b.WarehouseId)
            .IsRequired();

        builder.Property(b => b.RegionId)
            .IsRequired();

        builder.Property(b => b.Address2)
            .HasMaxLength(200);

        builder.Property(b => b.Address3)
            .HasMaxLength(200);

        builder.Property(b => b.Phone)
            .HasMaxLength(15);

        builder.Property(b => b.Fax)
            .HasMaxLength(15);

        builder.Property(b => b.Email)
            .HasMaxLength(50);

        builder.Property(b => b.Tin)
            .HasMaxLength(12);

    }
}
