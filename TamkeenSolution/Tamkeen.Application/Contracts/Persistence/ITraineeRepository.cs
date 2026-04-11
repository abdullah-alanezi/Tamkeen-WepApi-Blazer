using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Domain.Entities;

namespace Tamkeen.Application.Contracts.Persistence
{
    public interface ITraineeRepository : IGenericRepository<Trainee> {

        Task<bool> AddTraineeAsync(Trainee trainee);
        Task<bool> UpdateTraineeAsync(Trainee trainee);
        Task<bool> DeleteTraineeAsync(int id);
        Task<Trainee?> GetTraineeByIdAsync(int id);
        Task<IReadOnlyList<Trainee>> GetAllTraineesAsync();
    }
}
