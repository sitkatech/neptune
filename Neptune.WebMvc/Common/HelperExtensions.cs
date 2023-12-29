namespace Neptune.WebMvc.Common;

public static class HelperExtensions
{
    public static string GetRequiredString(this RouteData routeData, string keyName)
    {
        object value;
        if (!routeData.Values.TryGetValue(keyName, out value))
        {
            throw new InvalidOperationException($"Could not find key with name '{keyName}'");
        }

        return value?.ToString();
    }
}