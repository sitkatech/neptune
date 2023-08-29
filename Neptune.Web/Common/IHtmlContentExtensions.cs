using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace Neptune.Web.Common;

public static class IHtmlContentExtensions
{

    public static string ToHtmlString(this IHtmlContent? content)
    {
        if (content == null)
            return "";

        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    public static HtmlString ToHTMLFormattedString(this string text)
    {

        if (string.IsNullOrWhiteSpace(text))
        {
            return new HtmlString(string.Empty);
        }

        var result = text;
        result = result.Replace("\r\n", "\n").Replace("\n", "<br/>");

        return new HtmlString(result);
    }
}