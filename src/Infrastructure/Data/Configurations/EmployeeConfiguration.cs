using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
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
