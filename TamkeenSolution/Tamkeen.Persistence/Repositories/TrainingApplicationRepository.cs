using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Contracts.Persistence;
using Tamkeen.Domain.Entities;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories
{
    public class TrainingApplicationRepository : GenericRepository<TrainingApplication>, ITrainingApplicationRepository
    {
        public TrainingApplicationRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<bool> SubmitApplicationAsync(TrainingApplication entity)
        {
            await AddAsync(entity);

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateApplicationStatusAsync(TrainingApplication entity)
        {
            Update(entity);

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveApplicationAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            Delete(entity);

            await SaveChangesAsync();
            return true;
        }

        public async Task<TrainingApplication?> GetApplicationDetailsAsync(int id) => await GetByIdAsync(id);
        public async Task<IReadOnlyList<TrainingApplication>> GetAllApplicationsAsync() => await GetAllAsync();
    }
}
