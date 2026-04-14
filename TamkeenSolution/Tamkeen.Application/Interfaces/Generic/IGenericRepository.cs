using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tamkeen.Application.Models.PaginatedList;

namespace Tamkeen.Application.Interfaces.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        // البحث عن عنصر واحد مع إمكانية جلب البيانات المرتبطة
        Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>>? whereCondition = null,
            params Expression<Func<T, object>>[] navigationProperties);

        // جلب القائمة كاملة مع دعم الـ Mapping لـ DTO (TDestination)
        Task<List<TDestination>> GetAllAsync<TDestination>(
            Expression<Func<T, bool>>? whereCondition = null,
            params Expression<Func<T, object>>[] navigationProperties) where TDestination : class;

        // دعم الترقيم (Pagination) - مهم جداً لـ Blazor
        Task<PaginatedList<TDestination>> GetAllPaginationAsync<TDestination>(
            int index,
            int pageSize = 10,
            Expression<Func<T, bool>>? whereCondition = null) where TDestination : class;

        // العمليات الأساسية
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        // الحفظ
        Task<int> SaveChangesAsync();
    }
}
