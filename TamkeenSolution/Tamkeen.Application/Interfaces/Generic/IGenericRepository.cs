using System.Linq.Expressions;
using Tamkeen.Application.Models.PaginatedList;

namespace Tamkeen.Application.Interfaces.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>>? whereCondition = null,
            params Expression<Func<T, object>>[] navigationProperties);

        Task<List<TDestination>> GetAllAsync<TDestination>(
            Expression<Func<T, bool>>? whereCondition = null,
            params Expression<Func<T, object>>[] navigationProperties) where TDestination : class;

        Task<PaginatedList<TDestination>> GetAllPaginationAsync<TDestination>(
            int pageIndex,      // أفضل من "index" لتكون واضحة
            int pageSize = 10,
            Expression<Func<T, bool>>? whereCondition = null) where TDestination : class;

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> SaveChangesAsync();  // حفظ التغييرات على مستوى الـ Unit of Work
    }
}