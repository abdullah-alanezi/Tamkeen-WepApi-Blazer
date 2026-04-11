using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Contracts.Persistence;
using Tamkeen.Domain.Entities;
using Tamkeen.Persistence.Repositories.Generic;
namespace Tamkeen.Persistence.Repositories
{
    public class InterviewRepository : GenericRepository<Interview>, IInterviewRepository
    {
        public InterviewRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<bool> ScheduleInterviewAsync(Interview entity)
        {
            await AddAsync(entity);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> RescheduleInterviewAsync(Interview entity)
        {
            Update(entity);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelInterviewAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            Delete(entity);

            await SaveChangesAsync();

            return true;
        }

        public async Task<Interview?> GetInterviewByIdAsync(int id) => await GetByIdAsync(id);
        public async Task<IReadOnlyList<Interview>> GetAllInterviewsAsync() => await GetAllAsync();
    }
}
