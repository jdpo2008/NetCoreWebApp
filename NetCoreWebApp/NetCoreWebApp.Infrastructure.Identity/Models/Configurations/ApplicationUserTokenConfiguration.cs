using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Infrastructure.Identity.Models.Configurations
{
    public class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> b)
        {
            // Primary key
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            // Limit the size of columns to use efficient database types
            b.Property(t => t.LoginProvider).HasMaxLength(125);
            b.Property(t => t.Name).HasMaxLength(125);
        }
    }
}
