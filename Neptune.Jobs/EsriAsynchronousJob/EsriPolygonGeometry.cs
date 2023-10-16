using System.Text.Json.Serialization;

namespace Neptune.Jobs.EsriAsynchronousJob
{
    public class EsriPolygonGeometry
    {
        [JsonPropertyName("rings")]
        public List<List<double[]>> Rings { get; set; }
    }
}