using System;
using System.Collections.Generic;
using System.Text;
using Delex_POS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delex_POS.Infrastructure.Data.Configurations;

internal class ProductSaleConfiguration : IEntityTypeConfiguration<ProductSale>
{
    public void Configure(EntityTypeBuilder<ProductSale> builder)
    {
        builder.Property(ps => ps.ProductId)
            .IsRequired();

        builder.Property(ps => ps.ShortName)
            .HasMaxLength(100);

        builder.Property(ps => ps.IsSellThisItem)
            .HasDefaultValue(true);

        builder.Property(ps => ps.IsSellItemInWeb)
            .HasDefaultValue(false);

        builder.Property(ps => ps.TaxType)
            .IsRequired();

        builder.Property(ps => ps.MarkUp)
            .IsRequired();

        builder.Property(ps => ps.StandardCost)
            .IsRequired();

        builder.Property(ps => ps.LastPrice)
            .IsRequired();

        builder.Property(ps => ps.SeniorTax)
            .HasDefaultValue(false);

        builder.Property(ps => ps.PwdTax)
            .HasDefaultValue(false);

        builder.Property(ps => ps.IsSubjectToAmusement)
            .HasDefaultValue(false);

        builder.Property(ps => ps.IsSubjectToSoloParentDiscount)
            .HasDefaultValue(false);

    }
}
