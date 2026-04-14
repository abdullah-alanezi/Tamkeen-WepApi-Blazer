using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Interview;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Interview
{
    public class InterviewRepository : GenericRepository<Domain.Entities.Interview.Interview>, IInterviewRepository
    {
        public InterviewRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<bool> ScheduleInterviewAsync(Domain.Entities.Interview.Interview entity)
        {
            await AddAsync(entity);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> RescheduleInterviewAsync(Domain.Entities.Interview.Interview entity)
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

        public async Task<Domain.Entities.Interview.Interview?> GetInterviewByIdAsync(int id) => await GetByIdAsync(id);
        public async Task<IReadOnlyList<Domain.Entities.Interview.Interview>> GetAllInterviewsAsync() => await GetAllAsync();
    }
}
