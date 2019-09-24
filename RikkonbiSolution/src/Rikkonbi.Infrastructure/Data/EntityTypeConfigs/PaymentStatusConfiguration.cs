using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Core.Entities;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

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
        }
    }
}