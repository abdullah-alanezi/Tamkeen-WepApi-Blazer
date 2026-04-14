using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Persistence.Repositories.Generic;
namespace Tamkeen.Persistence.Repositories.Trainee
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
