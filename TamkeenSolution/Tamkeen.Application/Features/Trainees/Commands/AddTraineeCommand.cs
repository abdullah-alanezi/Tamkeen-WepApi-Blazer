using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.DTOs;

namespace Tamkeen.Application.Features.Trainees.Commands
{
    public class AddTraineeCommand : TraineeDto, IRequest<Result<TraineeDto>>
    {
        public AddTraineeCommand(TraineeDto trainee)
        {
            FirstName = trainee.FirstName;
            LastName = trainee.LastName;
            Email = trainee.Email;
            PhoneNumber = trainee.PhoneNumber;
            University = trainee.University;
            Major = trainee.Major;
            
        }

        
    }

    public class AddTraineeCommandHandler : IRequestHandler<AddTraineeCommand, Result<TraineeDto>>
    {

        private readonly ITraineeRepository _traineeRepository;

        public AddTraineeCommandHandler(ITraineeRepository traineeRepository)
        {
            _traineeRepository = traineeRepository;
        }
        public async Task<Result<TraineeDto>> Handle(AddTraineeCommand request, CancellationToken cancellationToken)
        {
            
            return Result<TraineeDto>.Success(await _traineeRepository.AddTraineeAsync(request));
             

        }
    }
}
