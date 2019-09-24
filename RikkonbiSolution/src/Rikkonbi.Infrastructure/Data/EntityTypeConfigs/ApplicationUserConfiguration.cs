using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Infrastructure.Identity;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.UserName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.SecurityStamp)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Avatar)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.EmailConfirmed)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LockoutEnabled)
                .IsRequired();

            builder.Property(u => u.AccessFailedCount)
                .IsRequired();

            builder.Property(u => u.CreatedOn)
                .IsRequired();

            builder.Property(u => u.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.UpdatedOn)
                .IsRequired(false);

            builder.Property(u => u.UpdatedBy)
                .HasMaxLength(100)
                .IsRequired(false);

            // Each User can have many entries in the UserRole join table
            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}