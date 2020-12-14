using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Infrastructure.Identity.Models.Configurations
{
    public class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserLogin> b)
        {
            // Primary key
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Limit the size of columns to use efficient database types
            b.Property(l => l.LoginProvider).HasMaxLength(125);
            b.Property(l => l.ProviderKey).HasMaxLength(125);
        }
    }
}
