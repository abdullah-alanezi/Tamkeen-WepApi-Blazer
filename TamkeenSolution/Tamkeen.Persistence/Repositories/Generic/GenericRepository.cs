using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Application.Models.PaginatedList;

namespace Tamkeen.Persistence.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        protected readonly IMapper _mapper;

        public GenericRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _mapper = mapper;
        }

        public async Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>>? whereCondition = null,
            params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var navProp in navigationProperties)
                query = query.Include(navProp);

            if (whereCondition != null)
                query = query.Where(whereCondition);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TDestination>> GetAllAsync<TDestination>(
            Expression<Func<T, bool>>? whereCondition = null,
            params Expression<Func<T, object>>[] navigationProperties) where TDestination : class
        {
            IQueryable<T> query = _dbSet;

            // ✅ تحسين: الـ Includes فقط عند الحاجة (ولكن ProjectTo يستغني عنها غالباً)
            if (navigationProperties.Any())
            {
                foreach (var navProp in navigationProperties)
                    query = query.Include(navProp);
            }

            if (whereCondition != null)
                query = query.Where(whereCondition);

            return await query
                .ProjectTo<TDestination>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PaginatedList<TDestination>> GetAllPaginationAsync<TDestination>(
            int pageIndex,
            int pageSize = 10,
            Expression<Func<T, bool>>? whereCondition = null) where TDestination : class
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            IQueryable<T> query = _dbSet;

            if (whereCondition != null)
                query = query.Where(whereCondition);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TDestination>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PaginatedList<TDestination>(items, totalCount, pageIndex, pageSize);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            // ✅ تصحيح: نعلق الكيان على الـ Context ونحدد حالته كـ Modified
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}