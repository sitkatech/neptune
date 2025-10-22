using System;

namespace Neptune.API.Services.Attributes;

/// <summary>
/// Attribute used by <see cref="EntityNotFoundMiddleware"/> that will automatically
/// return a 404 response from the API if the EntityType with the primary key value
/// does not exist.
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class EntityNotFoundAttribute(Type entityType, string pkStringInRoute) : Attribute
{
    /// <summary>
    /// The Entity Type to retrieve from the DbContext
    /// </summary>
    public Type EntityType { get; set; } = entityType;

    /// <summary>
    /// The primary key string in the route to retrieve the entity with.
    /// </summary>
    public string PKStringInRoute { get; set; } = pkStringInRoute;

    /// <summary>
    /// Filter order (lower runs first)
    /// </summary>
    public int Order { get; set; } = -1000;
}