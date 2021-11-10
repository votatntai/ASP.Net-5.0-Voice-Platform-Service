using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using VoicePlatform.Application.Configurations.Middleware;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Helpers;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Application.Controllers
{
    [ApiController]
    [Route("api/v1/artists")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly Validation validation = new Validation();

        public ArtistsController(IArtistService service)
        {
            _artistService = service;
        }

        /// <summary>
        /// Get artist by id
        /// </summary>
        [Authorize("Artist", "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetArtistById(Guid id)
        {
            var artist = await _artistService.GetArtistById(id);
            if (artist is null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        /// <summary>
        /// Get all artist used by admin
        /// </summary>
        [Authorize("Admin")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> AdminSearchArtist([FromQuery] Pagination pagination, [FromQuery] AdminSearchArtist search)
        {
            var artists = await _artistService.AdminSearchArtist(pagination, search.SearchString, search.Filter, search.Sort);
            var art = (Response)artists;
            if (art.TotalRow > 0)
            {
                return Ok(artists);
            }
            return NotFound();
        }

        /// <summary>
        /// Get all artist who are ready
        /// </summary>
        [Authorize("Customer")]
        [HttpGet]
        [Route("ready")]
        public async Task<IActionResult> CustomerSearchArtis([FromQuery] Pagination pagination, [FromQuery] CustomerSearchArtist search)
        {
            var artists = await _artistService.CustomerSearchArtist(pagination, search.SearchString, search.Filter, search.isAsc);
            var art = (Response)artists;
            if (art.TotalRow > 0)
            {
                return Ok(artists);
            }
            return NotFound();
        }

        /// <summary>
        /// Register artist
        /// </summary>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAnArtist([FromBody][Required] ArtistRequest artist)
        {
            var errors = validation.ValidateArtist(artist, "RegisterArtist");
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }
            else
            {
                var response = await _artistService.RegisterAnArtist(artist);

                if (response is List<Error>)
                {
                    return BadRequest(response);
                }
                if (response is QuickArtistResponse)
                {
                    return Created("", response);
                }
            }
            return StatusCode(500, "Internal Server Error");
        }

        /// <summary>
        /// Update artist
        /// </summary>
        [Authorize("Artist")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateArtist([FromQuery][Required] UpdateUserRequest artist)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var errors = validation.ValidateUpdateUser(artist, "UpdateArtist");
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }
            else
            {
                var response = await _artistService.UpdateArtist(artist, userId);
                if (response is List<Error>)
                {
                    return BadRequest(response);
                }
                else if (response is QuickArtistResponse)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }

        /// <summary>
        /// Update properties for artist
        /// </summary>
        [Authorize("Artist")]
        [HttpPut]
        [Route("subupdate")]
        public async Task<IActionResult> UpdateSubArtist([FromQuery][Required] SubArtistRequest artist)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var response = await _artistService.UpdateSubArtist(artist, userId);
            return response ? NoContent() : BadRequest();
        }

        /// <summary>
        /// Change artist status
        /// </summary>
        [Authorize("Admin")]
        [HttpPut]
        [Route("{id}/status")]
        public async Task<IActionResult> UpdateArtistStatus(Guid id, [Required] UserStatus status)
        {
            var adminId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            bool result = await _artistService.ChangeStatusAstirt(id, adminId, status);
            return result ? NoContent() : StatusCode(500, "Internal Server Error");
        }

        /// <summary>
        /// Update artist avatar
        /// </summary>
        [Authorize("Artist")]
        [HttpPut]
        [Route("avatar")]
        public async Task<IActionResult> UpdateAvatar([FromQuery][Required] string url)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var error = validation.ValidateUrl(url, "UpdateAvatar");
            if (error == null)
            {
                bool result = await _artistService.UpdateAvatar(userId, url);
                return result ? NoContent() : StatusCode(500, "Internal Server Error");
            }
            else
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Update artist bio
        /// </summary>
        [Authorize("Artist")]
        [HttpPut]
        [Route("bio")]
        public async Task<IActionResult> UpdateBio([FromBody][Required] string bio, [FromQuery][Required] double? price)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());

            bool result = await _artistService.UpdateBio(userId, bio, price);
            return result ? NoContent() : StatusCode(500, "Internal Server Error");
        }

        /// <summary>
        /// Update artist password
        /// </summary>
        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> UpdatePassword([FromBody][Required] UpdatePassword upd)
        {
            var error = validation.ValidatePassword(upd.Password, "UpdatePassword");
            if (error is null)
            {
                bool result = await _artistService.UpdatePasword(upd.Email, upd.Password);
                return result ? NoContent() : NotFound();
            }
            return BadRequest(error);
        }

        /// <summary>
        /// Get artist voice demo
        /// </summary>
        [Authorize("Artist")]
        [HttpPost]
        [Route("voice-demo")]
        public async Task<IActionResult> AddVoiceDemo([FromQuery][Required] string url)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var error = validation.ValidateUrl(url, "AddVoiceDemo");
            if (error != null)
            {
                return BadRequest(error);
            }
            bool result = await _artistService.AddVoiceDemo(userId, url);
            return result ? NoContent() : BadRequest();
        }

        /// <summary>
        /// Delete artist voice demo
        /// </summary>
        [Authorize("Artist")]
        [HttpDelete]
        [Route("voice-demo")]
        public async Task<IActionResult> DeleteVoiceDemo([FromQuery][Required] string url)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());

            bool result = await _artistService.DeleteVoiceDemo(userId, url);
            return result ? NoContent() : BadRequest();
        }

        /// <summary>
        /// Get all ratings in artist
        /// </summary>
        [Authorize("Customer", "Artist", "Admin")]
        [HttpGet]
        [Route("{id}/rating")]
        public async Task<IActionResult> GetListRating(Guid id, [FromQuery] Pagination pagination, [FromQuery] RatingsRequest search)
        {
            var ratings = await _artistService.GetListRating(id, pagination, search.Filter, search.Sort);
            if (ratings != null)
            {
                if (ratings.TotalRow > 0)
                {
                    return Ok(ratings);
                }
            }
            return NotFound();
        }
    }
}
