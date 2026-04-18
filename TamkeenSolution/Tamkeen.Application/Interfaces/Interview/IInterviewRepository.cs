using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Interview;

namespace Tamkeen.Application.Interfaces.Interview
{
    public interface IInterviewRepository {

        Task<bool> ScheduleInterviewAsync(InterviewDto interview);
        Task<bool> RescheduleInterviewAsync(InterviewDto interview);
        Task<bool> CancelInterviewAsync(Guid id);
        Task<InterviewDto?> GetInterviewByIdAsync(Guid id);
        Task<List<InterviewDto>> GetAllInterviewsAsync();

    }
}
