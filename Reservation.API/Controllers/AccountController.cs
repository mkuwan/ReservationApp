using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservation.Application.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Reservation.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]    // not require authorization
    public class AccountController : ControllerBase
    {
        // POST api/<ValuesController>
        [HttpPost("client/login")]
        public async Task<ActionResult<ClientViewModel>> ClientLogin([FromBody] ClientViewModel value, string password)
        {
            throw new NotImplementedException();
        }

        [HttpPost("manager/login")]
        public async Task<ActionResult<ManagerViewModel>> ManagerLogin([FromBody] ManagerViewModel value)
        {
            throw new NotImplementedException();
        }

    }
}
