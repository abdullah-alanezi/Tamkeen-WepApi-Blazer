using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Contracts.Persistence;
using Tamkeen.Domain.Entities;
using Tamkeen.Persistence.Repositories.Generic;
namespace Tamkeen.Persistence.Repositories
{
    public class TraineeRepository : GenericRepository<Trainee>, ITraineeRepository
    {
        public TraineeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<bool> AddTraineeAsync(Trainee entity)
        {
            await AddAsync(entity);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateTraineeAsync(Trainee entity)
        {
            Update(entity);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTraineeAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;

            Delete(entity);
            await SaveChangesAsync();

            return true;
        }

        public async Task<Trainee?> GetTraineeByIdAsync(int id) => await GetByIdAsync(id);
        public async Task<IReadOnlyList<Trainee>> GetAllTraineesAsync() => await GetAllAsync();
    }
}
