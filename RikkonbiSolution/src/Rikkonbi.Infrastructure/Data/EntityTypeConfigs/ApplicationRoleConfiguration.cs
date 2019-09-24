using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rikkonbi.Infrastructure.Identity;

namespace Rikkonbi.Infrastructure.Data.EntityTypeConfigs
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(r => r.Description)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(r => r.CreatedOn)
                .IsRequired();

            builder.Property(r => r.CreatedBy)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.UpdatedOn)
                .IsRequired(false);

            builder.Property(r => r.UpdatedBy)
                .HasMaxLength(100)
                .IsRequired(false);

            // Each Role can have many entries in the UserRole join table
            builder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}