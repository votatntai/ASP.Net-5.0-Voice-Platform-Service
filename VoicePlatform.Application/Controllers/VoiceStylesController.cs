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
    [Route("api/v1/voice-styles")]
    [ApiController]
    public class VoiceStylesController : ControllerBase
    {
        private readonly IVoiceStyleService _voiceStyleService;
        public VoiceStylesController(IVoiceStyleService service)
        {
            _voiceStyleService = service;
        }

        /// <summary>
        /// Get list Voice Style
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchString"></param>
        /// <param name="isAsc"></param>
        /// <returns>A list of Voice Style</returns>
        /// <response code="200">Returns list Voice Style</response>
        /// <response code="404">List voice style null</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Authorize("Customer", "Artist", "Admin")]
        [ProducesResponseType(typeof(List<MiniReqRes>), 200)]
        [Route("")]
        public async Task<IActionResult> GetVoiceStyles([FromQuery] Pagination pagination, string searchString, bool isAsc)
        {
            var res = await _voiceStyleService.GetVoiceStyles(pagination, searchString, isAsc);
            if (res != null)
            {
                return res.TotalRow != 0 ? Ok(res) : NotFound();
            }
            return BadRequest();
        }

        /// <summary>
        /// Get voice style
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Voice Style</returns>
        /// <response code="200">Returns Voice Style</response>
        /// <response code="404">Voice Style not found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Authorize("Admin")]
        [Route("{id}")]
        [ProducesResponseType(typeof(MiniReqRes), 200)]
        public async Task<IActionResult> GetVoiceStyleById(Guid id)
        {
            var gender = await _voiceStyleService.GetVoiceStyleById(id);
            if (gender != null)
            {
                return Ok(gender);
            }
            return BadRequest();
        }

        /// <summary>
        /// Create voice style
        /// </summary>
        /// <param name="voiceStyle"></param>
        /// <returns>Voice Style</returns>
        /// <response code="201">Create success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Authorize("Admin")]
        [Route("")]
        [ProducesResponseType(typeof(Guid), 200)]
        public async Task<IActionResult> CreateVoiceStyle(string voiceStyle)
        {
            if (string.IsNullOrWhiteSpace(voiceStyle))
            {
                return BadRequest("EMPTY_VOICESTYLE");
            }
            var result = await _voiceStyleService.CreateVoiceStyle(voiceStyle);
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
        /// Update voice style
        /// </summary>
        /// <param name="voiceStyle"></param>
        /// <returns>Voice Style</returns>
        /// <response code="200">Update success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Authorize("Admin")]
        [Route("")]
        public async Task<IActionResult> UpdateVoiceStyle(MiniReqRes voiceStyle)
        {
            if (voiceStyle?.Id != Guid.Empty && voiceStyle?.Name != null)
            {
                bool result = await _voiceStyleService.UpdateVoiceStyle(voiceStyle);
                return result ? Ok() : BadRequest();
            }
            return BadRequest("EMPTY_VOICESTYLE");
        }

        /// <summary>
        /// Delete voice style
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Voice Style</returns>
        /// <response code="200">Delete success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Authorize("Admin")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteVoiceStyle(Guid id)
        {
            if (id != Guid.Empty)
            {
                bool result = await _voiceStyleService.DeleteVoiceStyle(id);
                return result ? Ok() : BadRequest();
            }
            return BadRequest("EMPTY_VOICESTYLE");
        }
    }
}
