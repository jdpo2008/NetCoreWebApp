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
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;

        public ProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        public Task<List<Product>> GetAll()
        {
            return _products.Include(p => p.Category).Include(p => p.Marca).ToListAsync();
        }

        public Task<Product> GetById(Guid id)
        {
            return _products.Include(p => p.Category).Include(p => p.Marca).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
