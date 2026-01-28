using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Neptune.API.Services.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Neptune.API.Services.Filter;

public sealed class OptionalAuthOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation is null || context?.ApiDescription is null) return;

        // CustomAttributes() is the most reliable way to see action/controller attributes
        var hasOptionalAuth =
            context.ApiDescription.CustomAttributes().OfType<OptionalAuthAttribute>().Any();

        if (!hasOptionalAuth) return;

        operation.Extensions ??= new Dictionary<string, IOpenApiExtension>();
        operation.Extensions["x-optional-auth"] = new JsonNodeExtension(JsonValue.Create(true));
    }
}