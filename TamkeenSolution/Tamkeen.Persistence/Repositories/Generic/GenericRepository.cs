using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Application.Models.PaginatedList; // تأكد من المسار الصحيح لكلاس PaginatedList

namespace Tamkeen.Persistence.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        private readonly IMapper _mapper;

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

            foreach (var navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            if (whereCondition != null)
                query = query.Where(whereCondition);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TDestination>> GetAllAsync<TDestination>(
            Expression<Func<T, bool>>? whereCondition = null,
            params Expression<Func<T, object>>[] navigationProperties) where TDestination : class
        {
            IQueryable<T> query = _dbSet;

            // إضافة الـ Includes إذا لزم الأمر (غالباً ProjectTo يتكفل بها تلقائياً)
            foreach (var navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            if (whereCondition != null)
                query = query.Where(whereCondition);

            // استخدام ProjectTo لتحويل البيانات إلى DTO مباشرة في SQL
            return await query.ProjectTo<TDestination>(_mapper.ConfigurationProvider)
                              .ToListAsync();
        }

        public async Task<PaginatedList<TDestination>> GetAllPaginationAsync<TDestination>(
            int index,
            int pageSize = 10,
            Expression<Func<T, bool>>? whereCondition = null) where TDestination : class
        {
            IQueryable<T> query = _dbSet;

            if (whereCondition != null)
                query = query.Where(whereCondition);

            // حساب العدد الإجمالي قبل التجزئة
            var count = await query.CountAsync();

            // جلب بيانات الصفحة المحددة مع التحويل لـ DTO
            var items = await query.Skip((index - 1) * pageSize)
                                   .Take(pageSize)
                                   .ProjectTo<TDestination>(_mapper.ConfigurationProvider)
                                   .ToListAsync();

            return new PaginatedList<TDestination>(items, count, index, pageSize);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
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