using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Delex_POS.Infrastructure.Data.Configurations;

public class GenericConfiguration : IEntityTypeConfiguration<Generic>
{
    public void Configure(EntityTypeBuilder<Generic> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Memo)            
            .HasMaxLength(100);

    }
}
