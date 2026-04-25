using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Application.Models.PaginatedList;
using Tamkeen.Core.Common;

using Tamkeen.Core.Models.Trainee.Response;

namespace Tamkeen.Application.Features.Trainees.Queries
{
    public class GetTraineesListQuery
        : IRequest<Result<List<TraineeResponse>>>
    {
    }

    public class GetTraineesListHandler
        : IRequestHandler<GetTraineesListQuery, Result<List<TraineeResponse>>>
    {
        private readonly ITraineeRepository _repo;

        public GetTraineesListHandler(ITraineeRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<List<TraineeResponse>>> Handle(GetTraineesListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _repo.GetAllTraineesAsync();
                return Result<List<TraineeResponse>>.Success(data);
            }
            catch (Exception ex)
            {
                return Result<List<TraineeResponse>>.Failure(ex.Message);
            }
        }
    }
}
