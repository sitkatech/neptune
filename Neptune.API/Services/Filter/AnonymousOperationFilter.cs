using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Nodes;

namespace Neptune.API.Services.Filter;

public sealed class AnonymousOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation is null || context?.ApiDescription is null) return;

        var endpointMetadata = context.ApiDescription.ActionDescriptor?.EndpointMetadata;

        var allowAnonymousFromMetadata =
            endpointMetadata?.OfType<IAllowAnonymous>().Any() == true;

        var mi = context.MethodInfo;
        var allowAnonymousFromReflection =
            mi?.GetCustomAttributes(true).OfType<IAllowAnonymous>().Any() == true ||
            (mi?.DeclaringType?.GetCustomAttributes(true).OfType<IAllowAnonymous>().Any() ?? false);

        if (allowAnonymousFromMetadata || allowAnonymousFromReflection)
        {
            // IMPORTANT: Extensions can be null
            operation.Extensions ??= new Dictionary<string, IOpenApiExtension>();

            operation.Extensions["x-anonymous"] =
                new JsonNodeExtension(JsonValue.Create(true));
        }
    }
}