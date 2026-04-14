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
        Task<bool> CancelInterviewAsync(int id);
        Task<InterviewDto?> GetInterviewByIdAsync(int id);
        Task<List<InterviewDto>> GetAllInterviewsAsync();

    }
}
