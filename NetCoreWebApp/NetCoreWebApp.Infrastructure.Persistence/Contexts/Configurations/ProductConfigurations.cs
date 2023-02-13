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
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Limit the size of columns to use efficient database types
            builder.Property(p => p.Name).IsRequired().HasMaxLength(25);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(250);
            //builder.Property(p => p.ImageUrl).HasMaxLength(1000);
            //builder.Property(p => p.Price).HasColumnType("decimal(18,4)");

            //builder.HasMany(u => u.Images).WithMany(p => p.Id);

           // builder.HasMany(p => p.Images).WithOne(s => s.Product).OnDelete(DeleteBehavior.Cascade);

            // Each Product can have one Category
            builder.HasOne(c => c.Category).WithMany().HasForeignKey(p => p.CategoryId).IsRequired();

            // Each Product can have one SubCategory
            builder.HasOne(c => c.SubCategory).WithMany().HasForeignKey(p => p.SubCategoryId).IsRequired();

            // Each Product can have one Marca
            //builder.HasOne(m => m.Marca).WithMany().HasForeignKey(p => p.MarcaId).IsRequired();
        }
    }
}
