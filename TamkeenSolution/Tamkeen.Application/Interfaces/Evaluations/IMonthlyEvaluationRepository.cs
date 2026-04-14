using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Evaluations;

namespace Tamkeen.Application.Interfaces.Evaluations
{
    public interface IMonthlyEvaluationRepository{

        Task<bool> CreateEvaluationAsync(MonthlyEvaluationDto evaluation);
        Task<bool> UpdateEvaluationAsync(MonthlyEvaluationDto evaluation);
        Task<MonthlyEvaluationDto?> GetEvaluationByIdAsync(Guid id);
        Task<List<MonthlyEvaluationDto>> GetAllEvaluationsAsync();

    }
}
