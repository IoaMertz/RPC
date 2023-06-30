using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculateController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CalculateResponseObject>> Calculate(
            [FromBody] CalculateRequestObject calculateRequestObject)
        {
            return Ok();
        }
    }
}
