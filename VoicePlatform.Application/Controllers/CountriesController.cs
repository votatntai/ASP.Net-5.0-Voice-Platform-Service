using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoicePlatform.Application.Configurations.Middleware;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Implementations;

namespace VoicePlatform.Application.Controllers
{
    [Route("api/v1/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService service)
        {
            _countryService = service;
        }

        /// <summary>
        /// Get countries
        /// </summary>
        [HttpGet]
        [Authorize("Customer", "Artist", "Admin")]
        [Route("")]
        [ProducesResponseType(typeof(List<MiniReqRes>), 200)]
        public async Task<IActionResult> GetCountries([FromQuery] Pagination pagination, string searchString, bool isAsc)
        {
            var res = await _countryService.GetCountries(pagination, searchString, isAsc);
            if (res != null)
            {
                return res.TotalRow != 0 ? Ok(res) : NotFound();
            }
            return BadRequest();
        }

        /// <summary>
        /// Get country by id
        /// </summary>
        [HttpGet]
        [Authorize("Admin")]
        [Route("{id}")]
        [ProducesResponseType(typeof(MiniReqRes), 200)]
        public async Task<IActionResult> GetCountryById(Guid id)
        {
            var gender = await _countryService.GetCountryById(id);
            if (gender != null)
            {
                return Ok(gender);
            }
            return BadRequest();
        }

        /// <summary>
        /// Create country
        /// </summary>
        [HttpPost]
        [Authorize("Admin")]
        [Route("")]
        public async Task<IActionResult> CreateCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
            {
                return BadRequest("EMPTY_COUNTRY");
            }
            var result = await _countryService.CreateCountry(country);
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
        /// Update country
        /// </summary>
        [HttpPut]
        [Authorize("Admin")]
        [Route("")]
        public async Task<IActionResult> UpdateCountry(MiniReqRes country)
        {
            if (country?.Id != Guid.Empty && country?.Name != null)
            {
                bool result = await _countryService.UpdateCountry(country);
                return result ? Ok() : BadRequest();
            }
            return BadRequest("EMPTY_COUNTRY");
        }

        /// <summary>
        /// Delete country
        /// </summary>
        [HttpDelete]
        [Authorize("Admin")]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            if (id != Guid.Empty)
            {
                bool result = await _countryService.DeleteCountry(id);
                return result ? Ok() : BadRequest();
            }
            return BadRequest("EMPTY_COUNTRY");
        }
    }
}
