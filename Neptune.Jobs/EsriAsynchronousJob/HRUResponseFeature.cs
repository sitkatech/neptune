using System.Text.Json.Serialization;

namespace Neptune.Jobs.EsriAsynchronousJob
{
    public class HRUResponseFeature
    {
        [JsonPropertyName("attributes")]
        public HRUResponseFeatureAttributes Attributes { get; set; }
        [JsonPropertyName("geometry")]
        public EsriPolygonGeometry Geometry { get; set; }
    }
}