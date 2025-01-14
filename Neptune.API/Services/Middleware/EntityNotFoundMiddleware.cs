using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Neptune.API.Services.Attributes;
using Neptune.EFModels.Entities;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace Neptune.API.Services.Middleware;

/// <summary>
/// Middleware that checks each <see cref="EntityNotFoundAttribute"/> on route actions
/// and adjusts the response status code to <see cref="HttpStatusCode.NotFound"/> when
/// the entity is not found
/// </summary>
public class EntityNotFoundMiddleware
{
    private readonly RequestDelegate _next;

    public EntityNotFoundMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, NeptuneDbContext dbContext)
    {
        var endpoint = context.GetEndpoint();
        var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();
        var notFoundAttributes = actionDescriptor?.MethodInfo.GetCustomAttributes<EntityNotFoundAttribute>().ToList();

        if (notFoundAttributes != null && notFoundAttributes.Any())
        {
            foreach (var entityNotFoundAttribute in notFoundAttributes)
            {
                // Get the entity ID from the route parameters
                if (context.Request.RouteValues.TryGetValue(entityNotFoundAttribute.PKStringInRoute, out var idObj)
                    && int.TryParse(idObj.ToString(), out var id))
                {
                    // Check if the entity exists in the database
                    var entityType = entityNotFoundAttribute.EntityType;
                    var entity = await dbContext.FindAsync(entityType, id);
                    if (entity == null)
                    {
                        // Return a 404 response if the entity doesn't exist
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        return;
                    }
                }
            }
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }

}