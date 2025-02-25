using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleProductConfiguration : IEntityTypeConfiguration<SaleProduct> {
    public void Configure(EntityTypeBuilder<SaleProduct> builder) {
        builder.ToTable("SaleProducts");

        builder.HasKey(sp => sp.Id);
        builder.Property(sp => sp.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(sp => sp.Quantity).IsRequired();
        builder.Property(sp => sp.Discount).IsRequired();
        builder.Property(sp => sp.TotalValue).IsRequired();
        builder.Property(sp => sp.TotalWithDiscount).IsRequired();

        builder.HasOne(sp => sp.Sale);
        builder.HasOne(sp => sp.Product);

        builder.Property(sp => sp.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
