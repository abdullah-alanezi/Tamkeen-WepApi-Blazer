using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tamkeen.Application.Contracts;

namespace Tamkeen.Persistence.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        // Add
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // Delete
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        // Exists
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        // FirstOrDefault
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        // Get All
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        // Get All with filter
        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        // Get By Id
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        // Update
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}