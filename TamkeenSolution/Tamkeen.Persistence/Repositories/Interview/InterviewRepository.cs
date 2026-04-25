using AutoMapper;
using Tamkeen.Application.Interfaces.Interview;
using Tamkeen.Core.Models.Interview.Request;
using Tamkeen.Core.Models.Interview.Response;
using Tamkeen.Domain.Entities.Interview;
using Tamkeen.Domain.Enums;
using Tamkeen.Persistence.Repositories.Generic;

namespace Tamkeen.Persistence.Repositories.Interview
{
    public class InterviewRepository
        : GenericRepository<Tamkeen.Domain.Entities.Interview.Interview>, IInterviewRepository
    {
        private readonly IMapper _mapper;

        public InterviewRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _mapper = mapper;
        }

        // 🟢 Schedule Interview
        public async Task<InterviewResponse> ScheduleAsync(InterviewCreateDto dto)
        {
            var entity = _mapper.Map<Tamkeen.Domain.Entities.Interview.Interview>(dto);

            entity.Status = InterviewStatus.Scheduled;

            await AddAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<InterviewResponse>(entity);
        }

        // 🟡 Reschedule Interview
        public async Task<InterviewResponse> RescheduleAsync(InterviewCreateDto dto)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
                throw new Exception("Interview not found");

            entity.ScheduledOn = dto.ScheduledOn;
            entity.Location = dto.Location;
            entity.MeetingLink = dto.MeetingLink;
            entity.InterviewerName = dto.InterviewerName;

            entity.Status = InterviewStatus.Scheduled;

            await UpdateAsync(entity);
            await SaveChangesAsync();

            return _mapper.Map<InterviewResponse>(entity);
        }

        // 🔴 Cancel Interview
        public async Task<bool> CancelAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return false;

            entity.Status = InterviewStatus.Canceled;

            await UpdateAsync(entity);
            return await SaveChangesAsync() > 0;
        }

        // 🔵 Get By ID
        public async Task<InterviewResponse?> GetByIdAsync(Guid id)
        {
            var entity = await FirstOrDefaultAsync(x => x.Id == id);

            return entity == null
                ? null
                : _mapper.Map<InterviewResponse>(entity);
        }

        // 🟣 Get All
        public async Task<List<InterviewResponse>> GetAllAsync()
        {
            return await GetAllAsync<InterviewResponse>();
        }
    }
}