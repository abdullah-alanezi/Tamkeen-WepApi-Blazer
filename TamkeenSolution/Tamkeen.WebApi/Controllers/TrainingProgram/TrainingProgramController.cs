using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tamkeen.Application.Features.Trainees.Commands;
using Tamkeen.Application.Features.TrainingProgram.Commands;
using Tamkeen.Application.Features.TrainingProgram.Queries;

namespace Tamkeen.WebApi.Controllers.TrainingProgram
{
    [ApiController] 
    [Route("api/[controller]/[Action]")]
    public class TrainingProgramController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TrainingProgramController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CteateProgram(AddTrainingProgramCommand command )
        {

            var result = await _mediator.Send(command);

            return Ok(result);


        }

        [HttpGet]

        public async Task<IActionResult> GetAllPrograms()
        {
            var result = await _mediator.Send(new GetAllTrainingProgramQuery());

            return Ok(result);
        }

    }
}
