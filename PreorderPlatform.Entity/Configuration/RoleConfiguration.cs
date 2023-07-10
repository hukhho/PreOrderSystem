using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Configuration
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
