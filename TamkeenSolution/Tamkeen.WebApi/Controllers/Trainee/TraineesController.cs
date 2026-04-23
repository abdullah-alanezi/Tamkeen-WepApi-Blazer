using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tamkeen.Application.Features.Trainees.Commands;
using Tamkeen.Application.Features.Trainees.Queries;
using Tamkeen.Core.Models.DTOs; // تأكد من المسار الصحيح للـ Query

namespace Tamkeen.WebApi.Controllers.Trainee
{
    [ApiController] // يخبر الـ Framework أن هذا الكنترولر مخصص للـ API
    [Route("api/[controller]/[Action]")] // المسار سيكون: api/Trainees
    public class TraineesController : ControllerBase // الـ API يرث من ControllerBase وليس Controller
    {
        private readonly IMediator _mediator;

        public TraineesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // إرسال الطلب لـ MediatR
            var result = await _mediator.Send(new GetTraineesListQuery());

            if (result.IsSuccess)
            {
                // نرجع البيانات الموجودة داخل الـ Result
                return Ok(result);
            }

            // في حال الفشل، نرجع رسالة الخطأ مع كود 400
            return BadRequest(result.ErrorMessage);
        }



        [HttpPost]
        public async Task<IActionResult> AddTrainee(TraineeDto trainee)
        {
            var command = new AddTraineeCommand(trainee);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

    }
}