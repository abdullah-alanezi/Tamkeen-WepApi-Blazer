using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Common;

using Tamkeen.Core.Models.Trainee.Request;
using Tamkeen.Core.Models.Trainee.Response;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Features.Trainees.Commands
{
    public class AddTraineeCommand : TraineeCreateDto, IRequest<Result<TraineeResponse>>
    {
    }

    public class AddTraineeCommandHandler
        : IRequestHandler<AddTraineeCommand, Result<TraineeResponse>>
    {
        private readonly ITraineeRepository _repo;

        public AddTraineeCommandHandler(ITraineeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<TraineeResponse>> Handle(AddTraineeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repo.AddTraineeAsync(request);
                return Result<TraineeResponse>.Success(result);
            }
            catch (Exception ex)
            {
                return Result<TraineeResponse>.Failure(ex.Message);
            }
        }
    }
}
