namespace Neptune.Common;

[AttributeUsage(AttributeTargets.Method)]
public class LogIgnoreAttribute : Attribute
{
    public LogIgnoreAttribute() {}
}