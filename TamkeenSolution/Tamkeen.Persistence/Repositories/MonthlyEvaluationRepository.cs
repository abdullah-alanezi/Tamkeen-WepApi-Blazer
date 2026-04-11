using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Contracts.Persistence;
using Tamkeen.Domain.Entities;
using Tamkeen.Persistence.Repositories.Generic;
namespace Tamkeen.Persistence.Repositories
{
    public class MonthlyEvaluationRepository : GenericRepository<MonthlyEvaluation>, IMonthlyEvaluationRepository
    {
        public MonthlyEvaluationRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<bool> CreateEvaluationAsync(MonthlyEvaluation entity)
        {
            await AddAsync(entity);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateEvaluationAsync(MonthlyEvaluation entity)
        {
            Update(entity);

            await SaveChangesAsync();

            return true;
        }

        public async Task<MonthlyEvaluation?> GetEvaluationByIdAsync(int id) => await GetByIdAsync(id);
        public async Task<IReadOnlyList<MonthlyEvaluation>> GetAllEvaluationsAsync() => await GetAllAsync();
    }
}
