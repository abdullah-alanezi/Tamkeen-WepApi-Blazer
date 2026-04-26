using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.TrainingProgram.Request;
using Tamkeen.Core.Models.TrainingProgram.Response;

namespace Tamkeen.Application.Features.TrainingProgram.Commands
{
    public class AddTrainingProgramCommand:TrainingProgramRequest,IRequest<Result<TrainingProgramResponse>>
    {
    }

    public class AddTrainingProgramCommandHandler : IRequestHandler<AddTrainingProgramCommand, Result<TrainingProgramResponse>>
    {
        private readonly ITrainingProgramRepository _repo;

        public AddTrainingProgramCommandHandler(ITrainingProgramRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result<TrainingProgramResponse>> Handle(AddTrainingProgramCommand request, CancellationToken cancellationToken)
        {
            var result = await _repo.AddAsync(request);

            return Result<TrainingProgramResponse>.Success(result);
        }
    }
}
