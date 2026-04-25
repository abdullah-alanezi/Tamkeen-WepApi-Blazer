using AutoMapper;
using Tamkeen.Application.Interfaces.Evaluations;
using Tamkeen.Core.Models.MonthlyEvaluation.Request;
using Tamkeen.Core.Models.MonthlyEvaluation.Response;
using Tamkeen.Domain.Entities.Evaluations;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Evaluations
{
    public class MonthlyEvaluationRepository
        : GenericRepository<MonthlyEvaluation>, IMonthlyEvaluationRepository
    {
        private readonly IMapper _mapper;

        public MonthlyEvaluationRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        // 🟢 CREATE
        public async Task<MonthlyEvaluationResponse> CreateAsync(MonthlyEvaluationCreateDto dto)
        {
            var entity = _mapper.Map<MonthlyEvaluation>(dto);

            await AddAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<MonthlyEvaluationResponse>(entity);
        }

        // 🔵 GET BY ID
        public async Task<MonthlyEvaluationResponse?> GetByIdAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            return entity == null
                ? null
                : _mapper.Map<MonthlyEvaluationResponse>(entity);
        }

        // 🟣 GET ALL
        public async Task<List<MonthlyEvaluationResponse>> GetAllAsync()
        {
            return await GetAllAsync<MonthlyEvaluationResponse>();
        }

        // 🟡 UPDATE
        public async Task<MonthlyEvaluationResponse> UpdateAsync(MonthlyEvaluationCreateDto dto)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
                throw new Exception("Evaluation not found");

            entity.Month = dto.Month;
            entity.Year = dto.Year;
            entity.AttendanceGrade = dto.AttendanceGrade;
            entity.PerformanceGrade = dto.PerformanceGrade;
            entity.Comments = dto.Comments;

            await UpdateAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<MonthlyEvaluationResponse>(entity);
        }
    }
}