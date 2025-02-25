using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale> {
    public void Configure(EntityTypeBuilder<Sale> builder) {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.Number).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Total).IsRequired();
        builder.Property(s => s.TotalDiscount).IsRequired();

        builder.HasOne(s => s.Customer);
        builder.HasMany(s => s.Products).WithOne(sp => sp.Sale).HasForeignKey(sp => sp.SaleId).OnDelete(DeleteBehavior.Cascade);

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
