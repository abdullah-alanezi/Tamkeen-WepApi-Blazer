using AutoMapper;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Models.Trainee.Request;
using Tamkeen.Core.Models.Trainee.Response;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Trainee
{
    public class TraineeRepository
        : GenericRepository<Domain.Entities.Trainee.Trainee>, ITraineeRepository
    {
        private readonly IMapper _mapper;

        public TraineeRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        // 🟢 ADD
        public async Task<TraineeResponse> AddTraineeAsync(TraineeCreateDto dto)
        {
            var entity = _mapper.Map< Tamkeen.Domain.Entities.Trainee.Trainee >(dto);

            await AddAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<TraineeResponse>(entity);
        }

        // 🟡 UPDATE
        public async Task<TraineeResponse> UpdateTraineeAsync(TraineeCreateDto dto)
        {
            var entity = _mapper.Map< Tamkeen.Domain.Entities.Trainee.Trainee >(dto);

            await UpdateAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<TraineeResponse>(entity);
        }

        // 🔴 DELETE
        public async Task<bool> DeleteTraineeAsync(Guid id)
        {
            var entity = new Tamkeen.Domain.Entities.Trainee.Trainee { Id = id };

            _dbSet.Attach(entity);
            _dbSet.Remove(entity);

            return await SaveChangesAsync() > 0;
        }

        // 🔵 GET BY ID
        public async Task<TraineeResponse?> GetTraineeByIdAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            return entity == null
                ? null
                : _mapper.Map<TraineeResponse>(entity);
        }

        // 🟣 GET ALL (أفضل استخدام GenericRepository)
        public async Task<List<TraineeResponse>> GetAllTraineesAsync()
        {
            return await GetAllAsync<TraineeResponse>();
        }
    }
}