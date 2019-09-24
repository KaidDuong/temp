using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class DeviceCategoryConfiguration : IEntityTypeConfiguration<DeviceCategory>
    {
        public void Configure(EntityTypeBuilder<DeviceCategory> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasMaxLength(300);

            builder.Property(c => c.CreatedOn)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.UpdatedBy)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(c => c.UpdatedOn)
                .IsRequired(false);

            builder.Property(c => c.IsDeleted)
                .IsRequired();

            builder.HasMany(c => c.Devices);
        }
    }
}