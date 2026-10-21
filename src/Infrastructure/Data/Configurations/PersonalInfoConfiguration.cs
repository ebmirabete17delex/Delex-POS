using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

internal class PersonalInfoConfiguration : IEntityTypeConfiguration<PersonalInfo>
{
    public void Configure(EntityTypeBuilder<PersonalInfo> builder)
    {
        builder.HasQueryFilter(pi => !pi.IsDeleted);

        builder.HasIndex(pi => pi.IsDeleted)
            .HasFilter("IsDeleted = 0");

        builder.Property(pi => pi.Title)
            .HasMaxLength(3);

        builder.Property(pi => pi.FirstName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(pi => pi.LastName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(pi => pi.MiddleName)
            .HasMaxLength(30);

        builder.Property(pi => pi.NameExt)
            .HasMaxLength(10);

        builder.Property(pi => pi.BirthDate)
            .IsRequired();

        builder.Property(pi => pi.Gender)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(pi => pi.CivilStatus)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(pi => pi.Email)
            .HasMaxLength(50);

        builder.Property(pi => pi.BillingAddress1)
            .HasMaxLength(200);

        builder.Property(pi => pi.BillingAddress2)
            .HasMaxLength(200);

        builder.Property(b => b.RegionCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.ProvinceCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.MunicipalityCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(b => b.BarangayCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(pi => pi.Phone)
            .HasMaxLength(15);

        builder.Property(pi => pi.Mobile)
            .HasMaxLength(15);

        builder.Property(pi => pi.Fax)
            .HasMaxLength(15);

        builder.Property(pi => pi.Tin)
            .HasMaxLength(15);

        builder.Property(pi => pi.ContactPerson)
            .HasMaxLength(100);

    }
}
