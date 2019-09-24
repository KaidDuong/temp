using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class BorrowConfiguration : IEntityTypeConfiguration<Borrow>
    {
        public void Configure(EntityTypeBuilder<Borrow> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();

            builder.Property(c => c.CreatedOn)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .IsRequired();

            builder.Property(c => c.UpdatedOn)
                .IsRequired(false);

            builder.Property(c => c.UpdatedBy)
                .IsRequired(false);

            builder.Property(c => c.BorrowOn)
                .IsRequired();

            builder.Property(c => c.ReturnOn)
                .IsRequired();

            builder.Property(c => c.DeviceId)
                   .IsRequired(false);
        }
    }
}