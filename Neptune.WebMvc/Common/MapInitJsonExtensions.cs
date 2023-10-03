using System.Text.Json;
using Microsoft.AspNetCore.Html;
using Neptune.Common.GeoSpatial;

namespace Neptune.WebMvc.Common;

public static class MapInitJsonExtensions
{
    public static HtmlString ToJsonHtmlString(this object classToSerialize)
    {
        var value = JsonSerializer.SerializeToDocument(classToSerialize, GeoJsonSerializer.DefaultSerializerOptions).RootElement.GetRawText();
        return new HtmlString(value);
    }
}