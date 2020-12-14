using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Infrastructure.Identity.Models.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> b)
        {
            // Primary key
            b.HasKey(u => u.Id);

            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            b.Property(u => u.FirstName).HasMaxLength(25).IsRequired();
            b.Property(u => u.LastName).HasMaxLength(25).IsRequired();
            b.Property(u => u.UserName).HasMaxLength(25).IsRequired();
            b.Property(u => u.NormalizedUserName).HasMaxLength(25);
            b.Property(u => u.Email).HasMaxLength(50).IsRequired();
            b.Property(u => u.NormalizedEmail).HasMaxLength(50);

            // The relationships between User and other entity types

            // Each User can have many UserClaims
            b.HasMany<ApplicationUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            b.HasMany<ApplicationUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            b.HasMany<ApplicationUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            b.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        }
    }
}
