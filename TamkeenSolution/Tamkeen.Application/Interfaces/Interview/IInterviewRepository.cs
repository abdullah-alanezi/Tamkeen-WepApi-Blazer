using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Core.Models.Interview.Request;
using Tamkeen.Core.Models.Interview.Response;
using Tamkeen.Domain.Entities.Interview;

namespace Tamkeen.Application.Interfaces.Interview
{
    public interface IInterviewRepository {

        Task<InterviewResponse> ScheduleAsync(InterviewCreateDto dto);

        Task<InterviewResponse> RescheduleAsync(InterviewCreateDto dto);

        Task<bool> CancelAsync(Guid id);

        Task<InterviewResponse?> GetByIdAsync(Guid id);

        Task<List<InterviewResponse>> GetAllAsync();

    }
}
