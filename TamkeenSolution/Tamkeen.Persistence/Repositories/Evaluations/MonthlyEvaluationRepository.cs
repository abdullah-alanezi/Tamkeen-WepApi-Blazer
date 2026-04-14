using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Evaluations;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Evaluations;
using Tamkeen.Persistence.Repositories.Generic;
namespace Tamkeen.Persistence.Repositories.Evaluations
{
    public class MonthlyEvaluationRepository : GenericRepository<MonthlyEvaluation>, IMonthlyEvaluationRepository
    {
        private readonly IMapper _mapper;
        public MonthlyEvaluationRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext,mapper) {
        
            _mapper = mapper;
        }

        public async Task<bool> CreateEvaluationAsync(MonthlyEvaluationDto entity)
        {
            var dbInsert = _mapper.Map<MonthlyEvaluation>(entity);
            await AddAsync(dbInsert);
            await SaveChangesAsync();

            return true;
        }

        public async Task<MonthlyEvaluationDto?> GetEvaluationByIdAsync(Guid id)
        {
            var dbResult = await FirstOrDefaultAsync(x => x.Id == id);

            var response = _mapper.Map<MonthlyEvaluationDto>(dbResult);
            
            return response;
        }

        public async Task<bool> UpdateEvaluationAsync(MonthlyEvaluationDto entity)
        {
            var dbInsert = _mapper.Map<MonthlyEvaluation>(entity);

            await UpdateAsync(dbInsert);

            await SaveChangesAsync();

            return true;
        }

        
       

        public async Task<List<MonthlyEvaluationDto>> GetAllEvaluationsAsync()
        {
            var respons = await GetAllAsync<MonthlyEvaluationDto>(whereCondition: null);

            //return respons;

            return respons;
        }
    }
}
