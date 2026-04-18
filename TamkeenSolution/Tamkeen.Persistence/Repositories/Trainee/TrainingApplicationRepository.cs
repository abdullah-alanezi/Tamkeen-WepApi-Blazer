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
    public class TrainingApplicationRepository : GenericRepository<TrainingApplication>, ITrainingApplicationRepository
    {
        private readonly IMapper _mapper;
        public TrainingApplicationRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext,mapper) {
        
            _mapper = mapper;
        }

        public async Task<bool> SubmitApplicationAsync(TrainingApplicationDto entity)
        {
            var dbInsert = _mapper.Map<TrainingApplication>(entity);
            await AddAsync(dbInsert);

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateApplicationStatusAsync(TrainingApplicationDto entity)
        {
            var dbInsert = _mapper.Map<TrainingApplication>(entity);
            await UpdateAsync(dbInsert);

            await SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveApplicationAsync(Guid id)
        {
            var dbInsert = await FirstOrDefaultAsync(x=>x.Id == id);
            if (dbInsert == null) return false;

            await DeleteAsync(dbInsert);

            await SaveChangesAsync();
            return true;
        }

        public async Task<TrainingApplicationDto?> GetApplicationDetailsAsync(Guid id) 
        {
            var dbResult = await FirstOrDefaultAsync(x => x.Id == id);
            if (dbResult == null) return null;
            var response = _mapper.Map<TrainingApplicationDto>(dbResult);
            return response;
        }
        

        public async Task<List<TrainingApplicationDto>>GetAllApplicationsAsync()
        {
            var dbResult = await GetAllAsync<TrainingApplicationDto>(whereCondition: null);

            return dbResult;
        }
    }
}
