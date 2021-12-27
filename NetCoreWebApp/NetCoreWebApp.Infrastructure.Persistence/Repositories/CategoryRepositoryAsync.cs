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
    public class CategoryRepositoryAsync : GenericRepositoryAsync<Category>, ICategoryRepositoryAsync
    {
        private readonly DbSet<Category> _categories;

        public CategoryRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _categories = dbContext.Set<Category>();
        }
    }
}
