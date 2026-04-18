using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Interfaces.Trainee
{
    public interface ITrainingApplicationRepository
    {

        Task<bool> SubmitApplicationAsync(TrainingApplicationDto application);
        Task<bool> UpdateApplicationStatusAsync(TrainingApplicationDto application);
        Task<bool> RemoveApplicationAsync(Guid id);
        Task<TrainingApplicationDto?> GetApplicationDetailsAsync(Guid id);
        Task<List<TrainingApplicationDto>> GetAllApplicationsAsync();
    }
}
