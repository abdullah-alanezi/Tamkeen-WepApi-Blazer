using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Core.Models.TrainingApplication.Request;
using Tamkeen.Core.Models.TrainingApplication.Response;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Interfaces.Trainee
{
    public interface ITrainingApplicationRepository
    {

        Task<TrainingApplicationResponse> SubmitAsync(TrainingApplicationCreateDto dto);

        Task<TrainingApplicationResponse?> GetByIdAsync(Guid id);

        Task<List<TrainingApplicationResponse>> GetAllAsync();

        Task<bool> DeleteAsync(Guid id);

        Task<TrainingApplicationResponse> UpdateStatusAsync(Guid id, string status);
    }
}
