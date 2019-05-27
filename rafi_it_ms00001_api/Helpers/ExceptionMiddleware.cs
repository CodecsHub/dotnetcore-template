using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using rafi_it_ms00001_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace rafi_it_ms00001_api.Helpers
{
    //<summary>
    // @title:  Custom Global Error Trapping
    // @todo: add dynamic language translator
    // @todo: dont use direct text return in the functions, define magic methods
    // @see: <root> startup.cs
    //</summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var logger = _logger.CreateLogger<ExceptionMiddleware>();
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new UtilityErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                //Message = "Internal Server Error from the custom middleware."
                Message = exception.Message
            }.ToString());
        }
    }
}
