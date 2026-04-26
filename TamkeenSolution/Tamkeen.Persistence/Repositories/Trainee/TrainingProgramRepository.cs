using AutoMapper;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Models.TrainingProgram.Request;
using Tamkeen.Core.Models.TrainingProgram.Response;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Domain.Enums;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Trainee
{
    public class TrainingProgramRepository
        : GenericRepository<TrainingProgram>, ITrainingProgramRepository
    {
        private readonly IMapper _mapper;

        public TrainingProgramRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        // 🟢 CREATE PROGRAM
        public async Task<TrainingProgramResponse> AddAsync(TrainingProgramRequest dto)
        {
            var entity = _mapper.Map<TrainingProgram>(dto);

            entity.Status = TrainingProgramStatus.Draft;

            await AddAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<TrainingProgramResponse>(entity);
        }

        // 🔵 GET BY ID
        public async Task<TrainingProgramResponse?> GetByIdAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            return entity == null
                ? null
                : _mapper.Map<TrainingProgramResponse>(entity);
        }

        // 🟣 GET ALL
        public async Task<List<TrainingProgramResponse>> GetAllAsync()
        {
            return await GetAllAsync<TrainingProgramResponse>();
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
    }
}