using System;
using Microsoft.AspNetCore.Http;
using Neptune.Common;
using Serilog;
using Serilog.Events;

namespace Neptune.API.Services.Logging
{
    public class LogHelper
    {
        public static LogEventLevel CustomGetLevel(HttpContext ctx, double _, Exception ex)
        {
            if (IsIgnoredEndpoint(ctx))
                return LogEventLevel.Debug; // Return Debug level logs (which won't get picked up by serilog)

            return ex != null
                ? LogEventLevel.Error
                : ctx.Response.StatusCode > 499
                    ? LogEventLevel.Error
                    : LogEventLevel.Information;
        }

        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            // Set all the common properties available for every request
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);

            // Only set it if available. You're not sending sensitive data in a querystring right?!
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }

            // Set the content-type of the Response at this point
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

            // Retrieve the IEndpointFeature selected for the request
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object) // endpoint != null
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
        }

        private static bool IsIgnoredEndpoint(HttpContext ctx)
        {
            var endpoint = ctx.GetEndpoint();
            if (endpoint == null) return false;
            if (endpoint?.Metadata?.GetMetadata<LogIgnoreAttribute>() != null)
            {
                return true;
            }
            return string.Equals(
                endpoint.DisplayName,
                "Health checks",
                StringComparison.Ordinal);
        }

    }
}
