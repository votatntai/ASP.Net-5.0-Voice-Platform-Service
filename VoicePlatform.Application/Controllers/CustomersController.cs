using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoicePlatform.Application.Configurations.Middleware;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Helpers;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Application.Controllers
{
    [ApiController]
    [Route("api/v1/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly Validation validation = new Validation();
        public CustomersController(ICustomerService service)
        {
            _customerService = service;
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        [Authorize("Admin", "Customer", "Artist")]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), 200)]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            return BadRequest("Your data is invalid");
        }

        /// <summary>
        /// Get all customer
        /// </summary>
        [Authorize("Admin")]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<CustomerResponse>), 200)]
        public async Task<IActionResult> GetAllCustomer([FromQuery] AdminSearchCustomer? search, [FromQuery] Pagination pagination)
        {
            var customer = await _customerService.GetAllCustomer(pagination, search.Filter, search.Sort, search.SearchString);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

        /// <summary>
        /// Register customer
        /// </summary>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody][Required] CustomerRequest customer)
        {
            var errors = validation.ValidateCustomer(customer, "RegisterCustomer");
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            else
            {
                var response = await _customerService.RegisterCustomer(customer);
                if (response is List<Error>)
                {
                    return BadRequest(response);
                }
                else if (response is CustomerResponse)
                {
                    return Created("", response);
                }
                else
                {
                    return StatusCode(500, "Internal Server Error");
                }
            }
        }

        /// <summary>
        /// Update customer avatar
        /// </summary>
        [Authorize("Customer")]
        [HttpPut]
        [Route("avatar")]
        public async Task<IActionResult> UpdateAvatar([FromQuery] string url)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var error = validation.ValidateUrl(url, "UpdateAvatar");
            if (error == null)
            {
                bool result = await _customerService.UpdateAvatar(userId, url);
                return result ? NoContent() : StatusCode(500, "Internal Server Error");
            }
            else
            {
                return BadRequest(error);
            }
        }

        /// <summary>
        /// Update customer password
        /// </summary>
        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> UpdatePassword([FromBody][Required] UpdatePassword upd)
        {
            var error = validation.ValidatePassword(upd.Password, "UpdatePassword");
            if (error is null)
            {
                bool result = await _customerService.UpdatePassword(upd.Email, upd.Password);
                return result ? NoContent() : NotFound();
            }
            return BadRequest(error);
        }

        /// <summary>
        /// Update full customer
        /// </summary>
        [Authorize("Customer")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCustomer([FromQuery] UpdateUserRequest customer)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var errors = validation.ValidateUpdateUser(customer, "UpdateCustomer");
            if (errors.Any())
            {
                return BadRequest(errors);
            }
            else
            {
                var response = await _customerService.UpdateCustomer(customer, userId);
                if (response is List<Error>)
                {
                    return BadRequest(response);
                }
                else if (response is CustomerResponse)
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
        /// Update customer status
        /// </summary>
        [Authorize("Admin")]
        [HttpPut]
        [Route("{id}/status")]
        public async Task<IActionResult> BannedCustomer(Guid id, UserStatus status)
        {
            var adminId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            bool result = await _customerService.UpdateStatusCustomer(id, adminId, status);
            return result ? NoContent() : StatusCode(500, "Internal Server Error");
        }

    }
}
