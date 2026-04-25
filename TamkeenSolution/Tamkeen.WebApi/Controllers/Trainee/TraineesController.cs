using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tamkeen.Application.Features.Trainees.Commands;
using Tamkeen.Application.Features.Trainees.Queries;

namespace Tamkeen.WebApi.Controllers.Trainee
{
    [ApiController] // يخبر الـ Framework أن هذا الكنترولر مخصص للـ API
    [Route("api/[controller]/[Action]")] // المسار سيكون: api/Trainees
    public class TraineesController : ControllerBase // الـ API يرث من ControllerBase وليس Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper; // تأكد من إضافة الـ IMapper إذا كنت تستخدم AutoMapper
        public TraineesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetTraineesListQuery());

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.ErrorMessage);
        }



        [HttpPost]
        public async Task<IActionResult> AddTrainee(AddTraineeCommand command)
        {
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.ErrorMessage);
        }

    }
}