using Microsoft.EntityFrameworkCore;
using NetCoreWebApp.Application.Interfaces.Repositories;
using NetCoreWebApp.Domain.Entities;
using NetCoreWebApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Infrastructure.Persistence.Repositories
{
    public class SubCategoryRepositoryAsync : GenericRepositoryAsync<SubCategory>, ISubCategoryRepositoryAsync
    {
        private readonly DbSet<SubCategory> _subcategories;

        public SubCategoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _subcategories = dbContext.Set<SubCategory>();
        }
    }
}
