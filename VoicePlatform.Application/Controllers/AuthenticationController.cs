using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Interfaces;

namespace Application.Controllers
{
    [Route("api/v1/authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        /// <summary>
        /// Login for admin
        /// </summary>
        [HttpPost]
        [Route("admin")]
        public async Task<IActionResult> AuthenticateAdmin([FromBody] [Required] AuthenticateRequest model)
        {
            var authenticate =  await _service.AuthenticateAdmin(model);
            if (authenticate is List<Error>)
            {
                return BadRequest(authenticate);
            }
            return Ok(authenticate);
        }

        /// <summary>
        /// Login for customer
        /// </summary>
        [HttpPost]
        [Route("customer")]
        public async Task<IActionResult> AuthenticateCustomer([FromBody][Required] AuthenticateRequest model)
        {
            var authenticate =  await _service.AuthenticateCustomer(model);
            if (authenticate is List<Error>)
            {
                return BadRequest(authenticate);
            }
            return Ok(authenticate);
        }

        /// <summary>
        /// Login for artist
        /// </summary>
        [HttpPost]
        [Route("artist")]
        public async Task<IActionResult> AuthenticateArtist([FromBody][Required] AuthenticateRequest model)
        {
            var authenticate =  await _service.AuthenticateArtist(model);
            if (authenticate is List<Error>)
            {
                return BadRequest(authenticate);
            }
            return Ok(authenticate);
        }

        /// <summary>
        /// Login for admin by google
        /// </summary>
        [HttpPost]
        [Route("admin/google")]
        public async Task<IActionResult> AuthenticateGoogleForAdmin([FromBody][Required] string token)
        {
            var authenticate =  await _service.AuthenticateGoogleAdmin(token);
            if (authenticate is List<Error>)
            {
                return BadRequest(authenticate);
            }
            return Ok(authenticate);
        }

        /// <summary>
        /// Login for customer by google
        /// </summary>
        [HttpPost]
        [Route("customer/google")]
        public async Task<IActionResult> AuthenticateGoogleForCustomer([FromBody][Required] string token)
        {
            var authenticate =  await _service.AuthenticateGoogleCustomer(token);
            if (authenticate is List<Error>)
            {
                return BadRequest(authenticate);
            }
            return Ok(authenticate);
        }

        /// <summary>
        /// Login for artist by google
        /// </summary>
        [HttpPost]
        [Route("artist/google")]
        public async Task<IActionResult> AuthenticateGoogleForArtist([FromBody][Required] string token)
        {
            var authenticate =  await _service.AuthenticateGoogleArtist(token);
            if (authenticate is List<Error>)
            {
                return BadRequest(authenticate);
            }
            return Ok(authenticate);
        }

    }
}
