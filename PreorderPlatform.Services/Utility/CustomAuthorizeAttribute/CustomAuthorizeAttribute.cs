using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using PreorderPlatform.Service.ViewModels.ApiResponse;
using System;
using System.Linq;
using System.Security.Claims;

namespace PreorderPlatform.Service.Utility.CustomAuthorizeAttribute
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                ApiResponse<object> apiResponse = new ApiResponse<object>(null, "Unauthorized", false, null);

                context.Result = new JsonResult(apiResponse) { StatusCode = 401 };
                return;
            }

            if (!string.IsNullOrEmpty(Roles))
            {
                var allowedRoles = Roles.Split(',');
                var hasRole = user.Claims.Any(c => c.Type == ClaimTypes.Role && allowedRoles.Contains(c.Value));

                if (!hasRole)
                {
                    ApiResponse<object> apiResponse = new ApiResponse<object>(null, "Forbidden", false, null);

                    context.Result = new JsonResult(apiResponse) { StatusCode = 403 };
                }
            }
        }
    }
}