using System;

namespace Neptune.API.Services.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class LogIgnoreAttribute : Attribute
{
    public LogIgnoreAttribute() {}
}