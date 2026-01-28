using System;

namespace Neptune.API.Services.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class OptionalAuthAttribute : Attribute
{
}