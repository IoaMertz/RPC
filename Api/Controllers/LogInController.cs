using Database.DbModels;
using Database.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly LogInService _logInService;
        public LogInController(LogInService logInService)
        {
            _logInService = logInService;
        }

        //this is wrong !!!
        [HttpPost]
        public async Task<ActionResult<string?>> GetId([FromBody] UserDbModel userDbModel)
        {
            var usersId = await _logInService.GetUsersId(userDbModel.UserName, userDbModel.Password);
            if(usersId == null)
            {
                return Ok("User not found");
            }
            
            return usersId;
        }
    }
}
