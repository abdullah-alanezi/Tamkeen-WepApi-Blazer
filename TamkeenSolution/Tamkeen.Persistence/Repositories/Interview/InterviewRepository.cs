using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Interview;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Interview
{
    public class InterviewRepository : GenericRepository<Domain.Entities.Interview.Interview>, IInterviewRepository
    {
        
        private readonly IMapper _mapper;
        public InterviewRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

            _mapper = mapper;
        }
        public async Task<bool> ScheduleInterviewAsync(InterviewDto entity)
        {
            var dbInsert = _mapper.Map<Domain.Entities.Interview.Interview>(entity);

            await AddAsync(dbInsert);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> RescheduleInterviewAsync(InterviewDto entity)
        {
            var dbInsert = _mapper.Map<Domain.Entities.Interview.Interview>(entity);

            await UpdateAsync(dbInsert);
            await SaveChangesAsync();

            return true;
        }

        public async Task<bool> CancelInterviewAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            await DeleteAsync(entity);

            await SaveChangesAsync();

            return true;
        }

        public async Task<InterviewDto?> GetInterviewByIdAsync(Guid id){
        
            var dbResult = await FirstOrDefaultAsync(x => x.Id == id);

            if (dbResult == null) return null;
            var response = _mapper.Map<InterviewDto>(dbResult);
            return response;
        }


        public async Task<List<InterviewDto>> GetAllInterviewsAsync()
        {

            var dbResult = await GetAllAsync<InterviewDto>();

            return dbResult;
        }
    }
}
