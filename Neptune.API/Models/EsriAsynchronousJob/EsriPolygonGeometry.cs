using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Neptune.API.Models.EsriAsynchronousJob
{
    public class EsriPolygonGeometry
    {
        [JsonPropertyName("rings")]
        public List<List<double[]>> Rings { get; set; }
    }
}