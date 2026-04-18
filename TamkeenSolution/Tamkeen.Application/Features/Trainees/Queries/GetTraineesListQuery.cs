using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tamkeen.Application.Interfaces.Trainee;
using Tamkeen.Application.Models.PaginatedList;
using Tamkeen.Core.Common;
using Tamkeen.Core.Models.DTOs;

namespace Tamkeen.Application.Features.Trainees.Queries
{
    public class GetTraineesListQuery : IRequest<Result<List<TraineeDto>>>
    {

    }

    public class GetTraineesListHandler : IRequestHandler<GetTraineesListQuery, Result<List<TraineeDto>>>
    {
        private readonly ITraineeRepository _traineeRepository;

        public GetTraineesListHandler(ITraineeRepository traineeRepository)
        {
            _traineeRepository = traineeRepository;
        }

        public async Task<Result<List<TraineeDto>>> Handle(GetTraineesListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _traineeRepository.GetAllTraineesAsync();

                // إرجاع نتيجة ناجحة
                return Result<List<TraineeDto>>.Success(data);
            }
            catch (Exception ex)
            {
                // إرجاع فشل مع رسالة الخطأ
                return Result<List<TraineeDto>>.Failure("حدث خطأ أثناء جلب البيانات: " + ex.Message);
            }
        }


    }
}
