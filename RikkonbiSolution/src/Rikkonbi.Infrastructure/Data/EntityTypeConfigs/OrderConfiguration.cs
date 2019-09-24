using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Core.Entities;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.OrderDate)
                .IsRequired();

            builder.Property(c => c.PaymentStatusId)
                .IsRequired();

            builder.Property(c => c.RegionId)
               .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(300)
                .IsRequired(false);

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

            builder.HasMany(x => x.Items)
                .WithOne();
        }
    }
}