using System.Text.Json;
using Microsoft.AspNetCore.Html;
using Neptune.Common.GeoSpatial;

namespace Neptune.Web.Common;

public static class MapInitJsonExtensions
{
    public static HtmlString ToHtmlString(this object classToSerialize)
    {
        var value = JsonSerializer.SerializeToDocument(classToSerialize, GeoJsonSerializer.CreateGeoJSONSerializerOptions()).RootElement.GetRawText();
        return new HtmlString(value);
    }
}