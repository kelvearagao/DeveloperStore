using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(s => s.SaleNumber)
                .IsRequired();

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.Property(s => s.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(s => s.Branch)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.IsCancelled)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne<User>() // 1:1 relationship with User
                .WithOne()
                .HasForeignKey<Sale>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Items) // 1:n relationship with SaleItem
                .WithOne()
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(s => s.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}