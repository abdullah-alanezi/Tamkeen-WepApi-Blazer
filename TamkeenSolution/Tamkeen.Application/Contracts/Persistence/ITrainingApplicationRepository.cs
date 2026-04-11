using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities;

namespace Tamkeen.Application.Contracts.Persistence
{
    public interface ITrainingApplicationRepository : IGenericRepository<TrainingApplication> {

        Task<bool> SubmitApplicationAsync(TrainingApplication application);
        Task<bool> UpdateApplicationStatusAsync(TrainingApplication application);
        Task<bool> RemoveApplicationAsync(int id);
        Task<TrainingApplication?> GetApplicationDetailsAsync(int id);
        Task<IReadOnlyList<TrainingApplication>> GetAllApplicationsAsync();
    }
}
