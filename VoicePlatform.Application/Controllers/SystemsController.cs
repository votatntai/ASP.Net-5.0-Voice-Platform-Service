using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoicePlatform.Application.Configurations.Middleware;
using VoicePlatform.Service.Helpers;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Application.Controllers
{
    [Route("api/v1/systems")]
    [ApiController]
    public class SystemsController : ControllerBase
    {
        private readonly ISystemService _systemService;
        private SendingNotification _pushNotification = new SendingNotification();

        public SystemsController(ISystemService service)
        {
            _systemService = service;
        }

        /// <summary>
        /// Get OTP
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        /// <returns>OTP</returns>
        /// <response code="200">Returns OTP</response>
        [HttpGet]
        [Route("otp")]
        public async Task<IActionResult> GetOTP([Required] string email, [Required] Role role)
        {
            var name = await _systemService.GetUserFormDB(email, role);
            if (name != null)
            {
                string otp = new Random().Next(999999).ToString();
                SendingMail sendingMail = new SendingMail();
                var result = await sendingMail.Send(email, otp, name);
                if (result is bool)
                {
                    return Ok(otp);
                }
                else
                {
                    return StatusCode(500, result);
                }
            }
            return NotFound("UserNull");
        }

        /// <summary>
        /// Add user token to database
        /// </summary>
        [Authorize("Customer", "Artist")]
        [HttpPost]
        [Route("save-token")]
        public async Task<IActionResult> SaveToken([Required] string role, [FromBody][Required] string token)
        {
            var userId = Guid.Parse(HttpContext.Items["UserId"]?.ToString());
            var result = await _systemService.SaveUserToken(userId, role, token);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// Subscribe token to topic user for web admin
        /// </summary>
        [Authorize("Admin")]
        [HttpPost]
        [Route("subscribe")]
        public async Task<IActionResult> SubscribeTokenToTopic([Required][FromBody] string token, [Required] string topic, [Required] bool isSubscribe)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("EmptyToken");
            }
            List<string> tokens = new List<string>();
            tokens.Add(token);
            if (isSubscribe)
            {
                return await _pushNotification.SubscribeTokenToTopic(tokens, topic) ? Ok() : BadRequest();
            }
            else
            {
                return await _pushNotification.UnsubscribeTokenToTopic(tokens, topic) ? Ok() : BadRequest();
            }

        }

        /// <summary>
        /// Test Notification
        /// </summary>
        [Authorize("Admin")]
        [HttpGet]
        [Route("notification")]
        public async Task<IActionResult> PushNotification()
        {
            SendingNotification pushNoti = new SendingNotification();
            await pushNoti.PushAll();
            return Ok();
        }
    }
}
