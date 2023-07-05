using Application.Commands;
using Application.Messages;
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
        public async Task<ActionResult<CalculationResponseMessage>> Calculate(
            [FromBody] CalculationRequestObject calculateRequestObject)
        {
            var ipv4Addres = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            var sendTask =   _mediator.Send(new CalculationRequestCommand(calculateRequestObject.ClientsID, ipv4Addres,
                calculateRequestObject.Number1,calculateRequestObject.Number2,calculateRequestObject.ServiceName));

            var completedTask = await Task.WhenAny(sendTask, Task.Delay(TimeSpan.FromMinutes(0.09)));
            

            if (completedTask == sendTask)
            {
                // The sendTask completed within the timeout duration
                return Ok(sendTask.Result);
            }

            return StatusCode(500, new { message="timeout"});
        }
    }
}
