using NetCoreWebApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllFiltered(Expression<Func<T, bool>> predicate);
        Task<int> CountAll();
        Task<int> CountAllFiltered(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
    }
}
