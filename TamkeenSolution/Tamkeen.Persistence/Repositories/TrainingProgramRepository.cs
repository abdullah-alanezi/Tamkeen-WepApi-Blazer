using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Contracts.Persistence;
using Tamkeen.Domain.Entities;
using Tamkeen.Persistence.Repositories.Generic;
namespace Tamkeen.Persistence.Repositories
{
    public class TrainingProgramRepository : GenericRepository<TrainingProgram>, ITrainingProgramRepository
    {
        public TrainingProgramRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<bool> AddTrainingProgramAsync(TrainingProgram entity)
        {
            if (entity.StartDate < DateTime.UtcNow)
                throw new Exception("لا يمكن إضافة برنامج بتاريخ قديم");

            await AddAsync(entity);

            await SaveChangesAsync();

            return true;
        }
    }
}
