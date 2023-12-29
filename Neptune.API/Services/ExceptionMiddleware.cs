using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Neptune.API.Services
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Error($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message;
            switch (exception)
            {
                case BadHttpRequestException badRequestException:
                    if (badRequestException.StatusCode == StatusCodes.Status413PayloadTooLarge)
                    {
                        context.Response.StatusCode = badRequestException.StatusCode;
                        message = "Bad Request. Payload too large.";
                    }
                    else
                    {
                        message = "Bad Request.";
                    }
                    break;
                default:
                    message = "Oops!  Something went wrong with your request.  Please try your request again.  If the problem persists, please email h2o.team@esassoc.com";
                    break;
            }

            await context.Response.WriteAsync(message);
        }
    }
}
