using System.Text.Json;
using Microsoft.AspNetCore.Html;
using Neptune.Common.GeoSpatial;

namespace Neptune.Web.Common;

public static class MapInitJsonExtensions
{
    public static HtmlString ToHtmlString(this MapInitJson mapInitJson)
    {
        var value = JsonSerializer.SerializeToDocument(mapInitJson, GeoJsonSerializer.CreateGeoJSONSerializerOptions(4326, 3, 2)).RootElement.GetRawText();
        return new HtmlString(value);
    }
}