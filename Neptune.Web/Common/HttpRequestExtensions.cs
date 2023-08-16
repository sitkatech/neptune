using Microsoft.Net.Http.Headers;

namespace Neptune.Web.Common;

public static class HttpRequestExtensions
{
    public static string? GetReferrer(this HttpRequest request)
    {
        return request.Headers[HeaderNames.Referer].ToString();
    }
}