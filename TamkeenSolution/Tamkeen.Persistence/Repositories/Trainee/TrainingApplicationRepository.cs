using AutoMapper;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Models.TrainingApplication.Request;
using Tamkeen.Core.Models.TrainingApplication.Response;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Domain.Enums;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Trainee
{
    public class TrainingApplicationRepository
        : GenericRepository<TrainingApplication>, ITrainingApplicationRepository
    {
        private readonly IMapper _mapper;

        public TrainingApplicationRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        // 🟢 CREATE APPLICATION
        public async Task<TrainingApplicationResponse> SubmitAsync(TrainingApplicationCreateDto dto)
        {
            var entity = _mapper.Map<TrainingApplication>(dto);

            entity.Status = ApplicationStatus.Pending;

            await AddAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<TrainingApplicationResponse>(entity);
        }

        // 🔵 GET BY ID
        public async Task<TrainingApplicationResponse?> GetByIdAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            return entity == null
                ? null
                : _mapper.Map<TrainingApplicationResponse>(entity);
        }

        // 🟣 GET ALL
        public async Task<List<TrainingApplicationResponse>> GetAllAsync()
        {
            return await GetAllAsync<TrainingApplicationResponse>();
        }

        // 🔴 DELETE
        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return false;

            await DeleteAsync(entity);

            return await SaveChangesAsync() > 0;
        }

        // 🟡 UPDATE STATUS ONLY
        public async Task<TrainingApplicationResponse> UpdateStatusAsync(Guid id, string status)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                throw new Exception("Application not found");

            if (!Enum.TryParse<ApplicationStatus>(status, true, out var parsedStatus))
                throw new Exception("Invalid status");

            entity.Status = parsedStatus;

            await UpdateAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<TrainingApplicationResponse>(entity);
        }
    }
}