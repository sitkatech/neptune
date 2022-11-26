using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace Neptune.Web.Common;

public interface IHasGeometry
{
    [JsonIgnore]
    Geometry Geometry { get; set; }
}