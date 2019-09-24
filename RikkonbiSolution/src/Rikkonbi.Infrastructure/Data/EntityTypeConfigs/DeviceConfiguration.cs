using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.ImageUrl)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasMaxLength(300);

            builder.Property(c => c.DeviceCategoryId)
                .IsRequired(false);

            builder.Property(c => c.CreatedOn)
                .IsRequired();

            builder.Property(c => c.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.UpdatedOn)
                .IsRequired(false);

            builder.Property(c => c.UpdatedBy)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(c => c.QrCodeContent)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.Property(c => c.QrCodeImageUrl)
                .IsRequired(false)
                .HasMaxLength(300);

            builder.Property(c => c.IsBorrowed)
                .IsRequired();

            builder.Property(c => c.IsDeleted)
                .IsRequired();

            builder.HasOne(p => p.DeviceCategory)
                .WithMany(c => c.Devices)
                .HasForeignKey(p => p.DeviceCategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(c => c.Borrows);
        }
    }
}