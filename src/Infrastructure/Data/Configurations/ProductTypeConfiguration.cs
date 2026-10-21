using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.HasQueryFilter(pt => !pt.IsDeleted);

        builder.HasIndex(pt => pt.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(pt => pt.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
