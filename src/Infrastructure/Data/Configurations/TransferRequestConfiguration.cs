using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class TransferRequestConfiguration : IEntityTypeConfiguration<TransferRequest> 
{
    public void Configure(EntityTypeBuilder<TransferRequest> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(x => x.BranchFrom)
            .IsRequired();

        builder.Property(x => x.BranchTo)
            .IsRequired();
    }
}
