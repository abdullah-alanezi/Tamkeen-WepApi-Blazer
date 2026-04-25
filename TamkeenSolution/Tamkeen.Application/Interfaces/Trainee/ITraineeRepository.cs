using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Core.Models.Trainee.Request;
using Tamkeen.Core.Models.Trainee.Response;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Interfaces.Trainee
{
    public interface ITraineeRepository{
        
        Task<TraineeResponse> AddTraineeAsync(TraineeCreateDto trainee);
        Task<TraineeResponse> UpdateTraineeAsync(TraineeCreateDto trainee);
        Task<bool> DeleteTraineeAsync(Guid id);
        Task<TraineeResponse?> GetTraineeByIdAsync(Guid id);
        Task <List<TraineeResponse>> GetAllTraineesAsync();
    }
}
