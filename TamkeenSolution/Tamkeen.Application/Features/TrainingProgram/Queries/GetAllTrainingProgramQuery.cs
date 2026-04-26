using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.TrainingProgram.Response;

namespace Tamkeen.Application.Features.TrainingProgram.Queries
{
    public class GetAllTrainingProgramQuery : IRequest<Result<List<TrainingProgramResponse>>>{ 

    }
    
    public class GetAllTrainingProgramQueryHandler : IRequestHandler<GetAllTrainingProgramQuery, Result<List<TrainingProgramResponse>>>
    {
        private readonly ITrainingProgramRepository _repo;

        public GetAllTrainingProgramQueryHandler(ITrainingProgramRepository repo)
        {
               _repo = repo;
        }
        public async Task<Result<List<TrainingProgramResponse>>> Handle(GetAllTrainingProgramQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync();

            return Result<List<TrainingProgramResponse>>.Success(result);
        }

        
    }


}
