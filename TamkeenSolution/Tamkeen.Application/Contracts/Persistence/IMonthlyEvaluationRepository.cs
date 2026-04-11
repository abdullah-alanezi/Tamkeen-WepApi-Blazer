using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities;

namespace Tamkeen.Application.Contracts.Persistence
{
    public interface IMonthlyEvaluationRepository : IGenericRepository<MonthlyEvaluation> {

        Task<bool> CreateEvaluationAsync(MonthlyEvaluation evaluation);
        Task<bool> UpdateEvaluationAsync(MonthlyEvaluation evaluation);
        Task<MonthlyEvaluation?> GetEvaluationByIdAsync(int id);
        Task<IReadOnlyList<MonthlyEvaluation>> GetAllEvaluationsAsync();

    }
}
