using Core.Utility;
using DAL.Repositories.IRepositories;
using Entity.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.ActionFilters
{
    public class LogActionFilter : IAsyncActionFilter
    {
        private readonly IUtilService _utilService;
        private readonly ILoggingRepository _loggingRepository;
        public LogActionFilter(IUtilService utilService, ILoggingRepository loggingRepository)
        {
            _utilService = utilService;
            _loggingRepository = loggingRepository;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var HttpContext = context.HttpContext;

            string traceIdentitier = HttpContext?.TraceIdentifier;
            string clientIP = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();
            string uri = HttpContext.Request.Host + HttpContext.Request.Path;

            string token = string.Empty;
            int? userId = null;
            if (!string.IsNullOrEmpty(HttpContext.Request.Headers["Authorization"]) && HttpContext.Request.Headers["Authorization"].ToString().Length > 7)
            {
                token = HttpContext.Request.Headers["Authorization"].ToString();
                userId = !string.IsNullOrEmpty(token) ? _utilService.GetUserIdFromToken(HttpContext.Request.Headers["Authorization"].ToString()) : null;
            }
            context.HttpContext.Request.Body.Position = 0;
            using var streamReader = new StreamReader(context.HttpContext.Request.Body);
            string bodyContent = await streamReader.ReadToEndAsync();
            context.HttpContext.Request.Body.Position = 0;

            RequestLog requestLog = new RequestLog()
            {
                TraceIdentifier = traceIdentitier,
                ClientIP = clientIP,
                URI = uri,
                Payload = bodyContent,
                Method = HttpContext.Request.Method,
                Token = token,
                UserId = userId,
                RequestDate = DateTime.Now
            };

            await next();

            int responseStatusCode = HttpContext.Response.StatusCode;
            ResponseLog responseLog = new ResponseLog()
            {
                TraceIdentifier = traceIdentitier,
                ResponseDate = DateTime.Now,
                StatusCode = responseStatusCode.ToString(),
                Token = token,
                UserId = userId
            };

            requestLog.ResponseLog = responseLog;
            await _loggingRepository.AddLog(requestLog);
        }
    }
}
