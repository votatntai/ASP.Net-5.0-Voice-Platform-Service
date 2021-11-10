using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoicePlatform.Application.Configurations.Middleware;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Interfaces;

namespace VoicePlatform.Application.Controllers
{
    [Route("api/v1/genders")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IGenderService _genderService;
        public GendersController(IGenderService service)
        {
            _genderService = service;
        }

        /// <summary>
        /// Get genders
        /// </summary>
        [HttpGet]
        [Authorize("Customer", "Artist", "Admin")]
        [Route("")]
        [ProducesResponseType(typeof(List<MiniReqRes>), 200)]
        public async Task<IActionResult> GetGenders([FromQuery] Pagination pagination, string searchString, bool isAsc)
        {
            var res = await _genderService.GetGenders(pagination, searchString, isAsc);
            if (res != null)
            {
                return res.TotalRow != 0 ? Ok(res) : NotFound();
            }
            return BadRequest();
        }

        /// <summary>
        /// Get gender by id
        /// </summary>
        [HttpGet]
        [Authorize("Admin")]
        [Route("{id}")]
        [ProducesResponseType(typeof(MiniReqRes), 200)]
        public async Task<IActionResult> GetGenderById(Guid id)
        {
            var gender = await _genderService.GetGenderById(id);
            if (gender != null)
            {
                return Ok(gender);
            }
            return BadRequest();
        }

        /// <summary>
        /// Create gender
        /// </summary>
        [HttpPost]
        [Authorize("Admin")]
        [Route("")]
        public async Task<IActionResult> CreateGenders(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
            {
                return BadRequest("EMPTY_GENDER");
            }
            var result = await _genderService.CreateGender(gender);
            if (result is Error)
            {
                return BadRequest(result);
            }
            else
            {
                if (result is Guid)
                {
                    return !result.Equals(Guid.Empty) ? Created("", result) : BadRequest();
                }
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Update gender
        /// </summary>
        [HttpPut]
        [Authorize("Admin")]
        [Route("")]
        public async Task<IActionResult> UpdateGenders(MiniReqRes gender)
        {
            if (gender?.Id != Guid.Empty && gender?.Name != null)
            {
                bool result = await _genderService.UpdateGender(gender);
                return result ? Ok() : BadRequest();
            }
            return BadRequest("EMPTY_GENDER");
        }

        /// <summary>
        /// Delete gender 
        /// </summary>
        [HttpDelete]
        [Authorize("Admin")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGenders(Guid id)
        {
            if (id != Guid.Empty)
            {
                bool result = await _genderService.DeleteGender(id);
                return result ? Ok() : BadRequest();
            }
            return BadRequest("EMPTY_GENDER");
        }
    }
}
