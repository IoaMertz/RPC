using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerAplication.Interfaces;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public ServicesController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var availableServicesNames = _serviceProvider.GetServices(typeof(ICalculation)).Select(ser => ser.GetType().Name);

            return Ok(availableServicesNames);
        }
    }
}
