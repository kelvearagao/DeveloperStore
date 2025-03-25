using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.HasKey(si => si.Id);

            builder.Property(si => si.Quantity)
                .IsRequired();

            builder.Property(si => si.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(si => si.Discount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(si => si.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(si => si.IsCancelled)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(si => si.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}