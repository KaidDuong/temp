using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Core.Entities;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.OrderId)
                .IsRequired();

            builder.Property(c => c.ProductId)
                .IsRequired();

            builder.Property(c => c.ProductName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Price)
               .HasColumnType("decimal (18, 0)")
               .IsRequired();

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(c => c.Quantity)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}