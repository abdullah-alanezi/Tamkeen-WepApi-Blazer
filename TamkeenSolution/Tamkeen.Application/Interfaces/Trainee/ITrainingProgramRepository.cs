
using Tamkeen.Core.Models.TrainingProgram.Request;
using Tamkeen.Core.Models.TrainingProgram.Response;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Interfaces.Trainee
{
    public interface ITrainingProgramRepository
    {
        Task<TrainingProgramResponse> AddAsync(TrainingProgramRequest dto);

        Task<TrainingProgramResponse?> GetByIdAsync(Guid id);

        Task<List<TrainingProgramResponse>> GetAllAsync();

        Task<bool> DeleteAsync(Guid id);
    }
}
