using Application.Commands;
using Application.Messages;
using Database.DbModels;
using Database.Interfaces;
using Database.Services;
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

        //this should not be here !!!!!(Client should not see DB or the repositories)
        private readonly IRepository<CalculationDbModel> _repo;

        //this should not be here 
        private readonly LogInService _logInService;

        public CalculateController(IMediator mediator, IRepository<CalculationDbModel> repo,LogInService logInService)
        {
            _mediator = mediator;

            //this is wrong
            _repo = repo;

            //this is wrong 
            _logInService = logInService;
        }

        [HttpPost]
        public async Task<ActionResult<CalculationResponseMessage>> Calculate(
            [FromBody] CalculationRequestObject calculateRequestObject)
        {

            //refactor 
             var logInValidationTask = await _mediator.Send(new UserValidationCommand(calculateRequestObject.ClientsID));

            //this should happen in the server
            if(await _logInService.ValidateUser(calculateRequestObject.ClientsID) == null)
            {
                return Ok("User Not Found");
            }


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



        //this is wrong it uses repos directly, this need to happen on the server 
        // command -> send() 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalculationDbModel>>> GetAll()
        {
            var kati = await _repo.GetAllAsync();
            Console.WriteLine("");
            return Ok( kati);
        }
    }
}
