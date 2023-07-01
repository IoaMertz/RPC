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
            await _mediator.Send(new CalculationRequestCommand(calculateRequestObject.Number,calculateRequestObject.Service));
            // there we sould wait for the result and send the response


            return Ok();
        }
    }
}
