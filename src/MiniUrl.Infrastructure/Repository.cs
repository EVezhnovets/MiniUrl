using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MiniUrl.ApplicationCore.Interfaces;
using System.Linq.Expressions;

namespace MiniUrl.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MiniUrlContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(MiniUrlContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            _ = await _dbSet.AddAsync(entity);
            _ = await _context.SaveChangesAsync();
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            _ = _dbSet.Remove(entity);
            _ = await _context.SaveChangesAsync();
            return;
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, 
            bool isTracking = false)
        {
            IQueryable<T> query = _dbSet;

            if (!isTracking) { query = query.AsNoTracking(); }
            if (include is not null) { query = include(query); }
            if (predicate is not null) { query = query.Where(predicate); }

            return orderBy is not null
                ? await orderBy(query).ToListAsync()
                : await query.ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, 
            bool isTracking = false)
        {
            IQueryable<T> query = _dbSet;

            if (!isTracking) { query = query.AsNoTracking(); }
            if (predicate is not null) { query = query.Where(predicate); }
            if (include is not null) { query = include(query); }

            return orderBy is not null
                ? await orderBy(query).FirstOrDefaultAsync()
                : await query.FirstOrDefaultAsync();
        }
    }
}