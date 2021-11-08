using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Core.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace API.CustomMiddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidateTokenMiddleware
    {
        private readonly RequestDelegate _next;
        public ValidateTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUtilService utilService)
        {
            if (httpContext.Request.Path.Value != Constants.LoginPath)
            {
                if (!(httpContext.Request.Path.Value == Constants.UserRegisterPath && httpContext.Request.Method.ToLower() == "post"))
                {
                    string tokenString = httpContext.Request.Headers[Constants.AuthorizationHeaderName].ToString();
                    if (!utilService.IsValidToken(tokenString))
                    {
                        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }
            }
            await _next.Invoke(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ValidateTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareClassTemplate(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateTokenMiddleware>();
        }
    }
}
