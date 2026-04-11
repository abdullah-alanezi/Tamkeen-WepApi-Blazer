using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities;

namespace Tamkeen.Application.Contracts.Persistence
{
    public interface IInterviewRepository : IGenericRepository<Interview> {

        Task<bool> ScheduleInterviewAsync(Interview interview);
        Task<bool> RescheduleInterviewAsync(Interview interview);
        Task<bool> CancelInterviewAsync(int id);
        Task<Interview?> GetInterviewByIdAsync(int id);
        Task<IReadOnlyList<Interview>> GetAllInterviewsAsync();

    }
}
