using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasQueryFilter(e => !e.IsDeleted);

        builder.HasIndex(e => e.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.EmployeeId)
            .HasMaxLength(20);

        builder.Property(e => e.PersonalInfoId)
            .IsRequired();

        builder.Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder.Property(e => e.BranchId)
            .IsRequired();
    }
}
