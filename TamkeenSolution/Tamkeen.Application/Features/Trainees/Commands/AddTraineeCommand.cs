using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.DTOs;
using Tamkeen.Domain.Entities.Trainee;

namespace Tamkeen.Application.Features.Trainees.Commands
{
    public class AddTraineeCommand : TraineeDto, IRequest<Result<TraineeDto>>
    {

        
        
    }

    public class AddTraineeCommandHandler : IRequestHandler<AddTraineeCommand, Result<TraineeDto>>
    {
        private readonly IMapper _mapper;

        private readonly ITraineeRepository _traineeRepository;

        public AddTraineeCommandHandler(ITraineeRepository traineeRepository, IMapper mapper)
        {
            _traineeRepository = traineeRepository;
            _mapper = mapper;
        }
        public async Task<Result<TraineeDto>> Handle(AddTraineeCommand request, CancellationToken cancellationToken)
        {
            

            return Result<TraineeDto>.Success(await _traineeRepository.AddTraineeAsync(_mapper.Map<TraineeDto>(request)));
             

        }
    }
}
