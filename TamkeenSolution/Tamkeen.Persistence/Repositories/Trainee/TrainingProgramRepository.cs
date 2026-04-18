using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Trainee;
using Tamkeen.Persistence.Repositories.Generic;
namespace Tamkeen.Persistence.Repositories.Trainee
{
    public class TrainingProgramRepository : GenericRepository<TrainingProgram>, ITrainingProgramRepository
    {
        private readonly IMapper _mapper;
        public TrainingProgramRepository(ApplicationDbContext dbContext,IMapper mapper)
            : base(dbContext,mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool> AddTrainingProgramAsync(TrainingProgramDto entity)
        {
            if (entity.StartDate < DateTime.UtcNow)
                throw new Exception("لا يمكن إضافة برنامج بتاريخ قديم");

            var dbInsert = _mapper.Map<TrainingProgram>(entity);

            await AddAsync(dbInsert);
            await SaveChangesAsync();

            return true;
        }
    }
}
