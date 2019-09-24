using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Core.Entities;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(c => c.Price)
                .HasColumnType("decimal(18,0)")
                .IsRequired();

            builder.Property(c => c.CategoryId)
                .IsRequired(false);
            
           	builder.Property(c => c.QrCodeContent)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(c => c.QrCodeImageUrl)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(c => c.MaxOrderQuantity)
                .IsRequired()
                .HasDefaultValue(10);

            builder.Property(c => c.CreatedOn)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.UpdatedOn)
                .IsRequired(false);

            builder.Property(c => c.UpdatedBy)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}