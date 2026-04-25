using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Core.Models.MonthlyEvaluation.Request;
using Tamkeen.Core.Models.MonthlyEvaluation.Response;
using Tamkeen.Domain.Entities.Evaluations;

namespace Tamkeen.Application.Interfaces.Evaluations
{
    public interface IMonthlyEvaluationRepository{

        Task<MonthlyEvaluationResponse> CreateAsync(MonthlyEvaluationCreateDto dto);

        Task<MonthlyEvaluationResponse> UpdateAsync(MonthlyEvaluationCreateDto dto);

        Task<MonthlyEvaluationResponse?> GetByIdAsync(Guid id);

        Task<List<MonthlyEvaluationResponse>> GetAllAsync();

    }
}
