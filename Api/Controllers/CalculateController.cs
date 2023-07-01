using Application.Commands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalculateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CalculationResponseObject>> Calculate(
            [FromBody] CalculationRequestObject calculateRequestObject)
        {
            var sendTask =  _mediator.Send(new CalculationRequestCommand(
                calculateRequestObject.Number,calculateRequestObject.Service));

            var completedTask = await Task.WhenAny(sendTask, Task.Delay(TimeSpan.FromMinutes(0.3)));

            if (completedTask == sendTask)
            {
                // The sendTask completed within the timeout duration
                var result = await sendTask;

                // Process the result and send the appropriate response
                return Ok("kati");
            }

            return Ok("skata");
        }
    }
}
