using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace auth_servce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IJwtService jwtservice = null;

        public AuthController(IJwtService _jwtservice)
        {
            jwtservice = _jwtservice;
        }

        [HttpPost("login")]
        public async Task<IActionResult> userLogin([FromBody] UserLogin _usr)
        {
            var token = new TokenDetail() {
                firstName = "John",
                lastName = "Doe",
            };
            var _token = await jwtservice.encode(token);
            return Ok(_token);
        }

        [HttpPost("decode")]
        public async Task<IActionResult> decode([FromBody] string _usr)
        {

            var _token = await jwtservice.decode(_usr);
            return Ok(_token);
        }
    }
}
