using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class ReturnToSellerConfiguration : IEntityTypeConfiguration<ReturnToSeller>
{
    public void Configure(EntityTypeBuilder<ReturnToSeller> builder)
    {
        builder.HasQueryFilter(ac => !ac.IsDeleted);

        builder.Property(ac => ac.BranchId)
            .IsRequired();

        builder.Property(ac => ac.SupplierId)
            .IsRequired();
    }
}
