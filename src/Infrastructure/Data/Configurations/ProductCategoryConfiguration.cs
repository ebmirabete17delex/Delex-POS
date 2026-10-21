using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasQueryFilter(pc => !pc.IsDeleted);

        builder.HasIndex(pc => pc.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(pc => pc.Name).IsRequired().HasMaxLength(100);
    }
}
