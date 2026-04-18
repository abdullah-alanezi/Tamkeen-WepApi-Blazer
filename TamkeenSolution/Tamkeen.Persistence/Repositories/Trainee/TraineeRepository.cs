using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Trainee
{
    public class TraineeRepository : GenericRepository<Domain.Entities.Trainee.Trainee>, ITraineeRepository
    {
        private readonly IMapper _mapper;

        public TraineeRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool> AddTraineeAsync(TraineeDto entity)
        {
            var traineeEntity = _mapper.Map<Domain.Entities.Trainee.Trainee>(entity);
            await AddAsync(traineeEntity);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTraineeAsync(TraineeDto entity)
        {
            var traineeEntity = _mapper.Map<Domain.Entities.Trainee.Trainee>(entity);
            await UpdateAsync(traineeEntity);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTraineeAsync(Guid id)
        {
            // ✅ تحسين الأداء: الحذف المباشر بدون قراءة أولاً
            var trainee = new Domain.Entities.Trainee.Trainee { Id = id };
            _dbSet.Attach(trainee);
            _dbSet.Remove(trainee);
            return await SaveChangesAsync() > 0;
        }

        public async Task<TraineeDto?> GetTraineeByIdAsync(Guid id)
        {
            var traineeEntity = await FirstOrDefaultAsync(x => x.Id == id);
            return traineeEntity == null ? null : _mapper.Map<TraineeDto>(traineeEntity);
        }

        public async Task<List<TraineeDto>> GetAllTraineesAsync()
        {
            return await GetAllAsync<TraineeDto>(whereCondition: null);
        }
    }
}