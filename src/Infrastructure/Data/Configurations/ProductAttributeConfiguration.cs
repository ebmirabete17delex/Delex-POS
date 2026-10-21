using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        builder.HasQueryFilter(pa => !pa.IsDeleted);

        builder.HasIndex(pa => pa.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(pa => pa.Name).IsRequired().HasMaxLength(100);
    }
}
