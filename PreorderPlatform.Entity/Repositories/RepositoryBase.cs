using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Entity.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly PreOrderSystemContext _context;
        private readonly DbSet<T> _dbSet;

        public IQueryable<T> Table => _dbSet;

        public RepositoryBase(PreOrderSystemContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet);
        }
        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            query = predicate==null ? query : query.Where(predicate);

            query = includes==null||includes.Length==0 ? query : includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllWithIncludeLoadRelatedEntitiesAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbSet;

            // Use the LoadRelatedEntities method to load all related entities
            query = query.LoadRelatedEntities(_context);

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public async Task<T> GetWithIncludeAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            //_dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}