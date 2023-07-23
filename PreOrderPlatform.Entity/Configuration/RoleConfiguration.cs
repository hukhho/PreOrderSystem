using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreOrderPlatform.Entity.Models;

namespace PreOrderPlatform.Entity.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //builder.HasKey(r => r.Id); // Set the primary key

            //builder.Property(r => r.Name)
            //    .IsRequired() // Name field is required
            //    .HasMaxLength(50); // Name field has a maximum length of 50 characters

            //// Configure the relationship with the User entity
            //builder.HasMany(r => r.Users)
            //    .WithOne(u => u.Role)
            //    .HasForeignKey(u => u.RoleId)
            //    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
