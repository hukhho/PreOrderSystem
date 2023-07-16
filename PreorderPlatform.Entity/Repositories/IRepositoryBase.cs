using Microsoft.EntityFrameworkCore.Query;
using PreorderPlatform.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> Table { get; }
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IQueryable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[]? includes);
        Task<IEnumerable<T>> GetAllWithIncludeLoadRelatedEntitiesAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetWithIncludeAsync(Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeExpressions);
        Task<bool> IsExistsByGuid(Guid id);
        Task UpdateAsync(T entity);
        Task UpdateMultiAsync(IEnumerable<T> entities);
    }
}
