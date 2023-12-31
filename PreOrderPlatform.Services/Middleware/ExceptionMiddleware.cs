﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PreOrderPlatform.Service.Services.Exceptions;
using PreOrderPlatform.Service.ViewModels.ApiResponse;

namespace PreOrderPlatform.Service.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";

                var response = new ApiResponse<string>(null, ex.Message, false);

                // Set the HTTP status code based on the type of exception
                if (ex is NotFoundException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else if (ex is AuthorizationException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                }
                else if (ex is ArgumentException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                if (ex is ServiceException)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else
                {
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
