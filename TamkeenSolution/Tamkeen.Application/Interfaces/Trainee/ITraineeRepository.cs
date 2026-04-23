using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Generic;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Interfaces.Trainee
{
    public interface ITraineeRepository{
        
        Task<TraineeDto> AddTraineeAsync(TraineeDto trainee);
        Task<TraineeDto> UpdateTraineeAsync(TraineeDto trainee);
        Task<bool> DeleteTraineeAsync(Guid id);
        Task<TraineeDto?> GetTraineeByIdAsync(Guid id);
        Task <List<TraineeDto>> GetAllTraineesAsync();
    }
}
