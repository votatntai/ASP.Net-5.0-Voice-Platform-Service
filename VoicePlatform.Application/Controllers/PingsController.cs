using VoicePlatform.Application.Configurations.Middleware;
using Microsoft.AspNetCore.Mvc;
using VoicePlatform.Utility.Enums;
using VoicePlatform.Data.Entities;
using VoicePlatform.Service.Helpers;

namespace Application.Controllers
{
    [ApiController]
    public class PingsController : ControllerBase
    {
        /// <summary>help you test the connection with server side</summary>
        [HttpGet]
        [Route("ping")]
        public IActionResult GetPing([FromQuery] Pagination pagination)
        {
            var result = new
            {
                Status = "Ping!!!",
            };
            return StatusCode(200, result);
        }

        [HttpGet]
        [Route("pong")]
        [Authorize("Admin")]
        public IActionResult GetPong()
        {
            var result = new
            {
                Status = "Pong!!!",
            };
            return StatusCode(200, result);
        }
    }
}
