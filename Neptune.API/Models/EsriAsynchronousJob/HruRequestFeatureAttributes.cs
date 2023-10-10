using System.Text.Json.Serialization;

namespace Neptune.API.Models.EsriAsynchronousJob
{
    public class HRURequestFeatureAttributes
    {
        [JsonPropertyName("OBJECTID")]
        public int ObjectID { get; set; }
        [JsonPropertyName("QueryFeatureID")]
        public int QueryFeatureID { get; set; } // actually wants to be a string..?
        [JsonPropertyName("Shape_Length")]
        public double Length { get; set; }
        [JsonPropertyName("Shape_Area")]
        public double Area { get; set; }
    }
}