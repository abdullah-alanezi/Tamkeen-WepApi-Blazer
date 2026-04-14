using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Interfaces.Trainee
{
    public interface ITrainingProgramRepository
    {
        Task<bool> AddTrainingProgramAsync(TrainingProgramDto trainingProgram);
    }
}
