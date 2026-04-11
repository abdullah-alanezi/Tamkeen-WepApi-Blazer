
using Tamkeen.Domain.Entities;

namespace Tamkeen.Application.Contracts.Persistence
{
    public interface ITrainingProgramRepository
    {
        Task<bool> AddTrainingProgramAsync(TrainingProgram trainingProgram);
    }
}
