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
    [Route("api/v1/usage-purposes")]

    [ApiController]
    public class UsagePurposesController : ControllerBase
    {
        private readonly IUsagePurposeService _usagePurposeService;
        public UsagePurposesController(IUsagePurposeService service)
        {
            _usagePurposeService = service;
        }

        /// <summary>
        /// Get list Usage Purpose
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchString"></param>
        /// <param name="isAsc"></param>
        /// <returns>A list of Usage Purpose</returns>
        /// <response code="200">Returns list</response>
        /// <response code="404">List null</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Authorize("Admin", "Customer", "Artist")]
        [ProducesResponseType(typeof(List<MiniReqRes>), 200)]
        [Route("")]
        public async Task<IActionResult> GetUsagePurposes([FromQuery] Pagination pagination, string searchString, bool isAsc)
        {
            var res = await _usagePurposeService.GetUsagePurposes(pagination, searchString, isAsc);
            if (res != null)
            {
                return res.TotalRow != 0 ? Ok(res) : NotFound();
            }
            return BadRequest();
        }

        /// <summary>
        /// Get Usage Purpose by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A Usage Purpose</returns>
        /// <response code="200">Returns Usage purpose</response>
        /// <response code="404">Usage purpose null</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Authorize("Admin")]
        [ProducesResponseType(typeof(MiniReqRes), 200)]
        [Route("{id}")]
        public async Task<IActionResult> GetUsagePurposeById(Guid id)
        {
            var usagePurpose = await _usagePurposeService.GetUsagePurposeById(id);
            if (usagePurpose != null)
            {
                return Ok(usagePurpose);
            }
            return BadRequest();
        }

        /// <summary>
        /// Create Usage Purpose
        /// </summary>
        /// <param name="usagePurpose"></param>
        /// <returns>Guid of Usage Purpose</returns>
        /// <response code="201">Create Ok</response>
        /// <response code="404">create error</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Authorize("Admin")]
        [Route("")]
        public async Task<IActionResult> CreateUsagePurpose(string usagePurpose)
        {
            if (string.IsNullOrWhiteSpace(usagePurpose))
            {
                return BadRequest("EMPTY_USAGEPURPOSE");
            }
            var result = await _usagePurposeService.CreateUsagePurpose(usagePurpose);
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
        /// Update Usage Purpose
        /// </summary>
        /// <param name="UsagePurpose"></param>
        /// <returns>Ok or Fail</returns>
        /// <response code="200">Update Ok</response>
        /// <response code="404">Usage purpose null</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Authorize("Admin")]
        [Route("")]
        public async Task<IActionResult> UpdateUsagePurpose(MiniReqRes UsagePurpose)
        {
            if (UsagePurpose?.Id != Guid.Empty && UsagePurpose?.Name != null)
            {
                bool result = await _usagePurposeService.UpdateUsagePurpose(UsagePurpose);
                return result ? Ok() : BadRequest();
            }
            return BadRequest("EMPTY_USAGEPURPOSE");
        }

        /// <summary>
        /// Delete Usage Purpose
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ok or false</returns>
        /// <response code="200">Delete Ok</response>
        /// <response code="404">Usage purpose null</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Authorize("Admin")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUsagePurpose(Guid id)
        {
            if (id != Guid.Empty)
            {
                bool result = await _usagePurposeService.DeleteUsagePurpose(id);
                return result ? Ok() : BadRequest();
            }
            return BadRequest("EMPTY_USAGEPURPOSE");
        }
    }
}
