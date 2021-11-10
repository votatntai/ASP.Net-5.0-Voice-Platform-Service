using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using VoicePlatform.Data.Application;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Application.Configurations.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public ICollection<string> Roles { get; set; }

        public AuthorizeAttribute(params string[] roles)
        {
            Roles = roles.Select(x => x.ToLower()).ToList();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var role = context.HttpContext.Items["Role"]?.ToString();
            var status = context.HttpContext.Items["Status"]?.ToString();

            if (role == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                if (status.Equals(UserStatus.Banned.ToString()))
                {
                    context.Result = new JsonResult(new { message = "Your account has been banned" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
                var isValid = false;
                if (Roles.Contains(role.ToLower()))
                {
                    isValid = true;
                }
                if (!isValid)
                {
                    context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
        }
    }
}
