using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreWebApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Infrastructure.Persistence.Contexts.Configurations
{
    public class SubCategoryConfigurations : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Limit the size of columns to use efficient database types
            builder.Property(p => p.Name).IsRequired().HasMaxLength(25);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(250);

            // Each SubCategory can have one Category
            builder.HasOne(c => c.Category).WithMany().HasForeignKey(p => p.CategoryId).IsRequired();

        }
    }
}
