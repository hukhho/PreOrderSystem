using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace PreOrderPlatform.Entity.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> Table { get; }
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IQueryable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[]? includes);
        //Task<IEnumerable<T>> GetAllWithIncludeLoadRelatedEntitiesAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetWithIncludeAsync(Expression<Func<T, bool>> predicate, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includeExpressions);
        Task<bool> IsExistsByGuid(Guid id);
        Task UpdateAsync(T entity);
        Task UpdateMultiAsync(IEnumerable<T> entities);
    }
}
