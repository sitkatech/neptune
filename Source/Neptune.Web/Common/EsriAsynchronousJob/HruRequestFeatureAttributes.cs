using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class HRURequestFeatureAttributes
    {
        [JsonProperty("OBJECTID")]
        public int ObjectID { get; set; }
        [JsonProperty("QueryFeatureID")]
        public int QueryFeatureID { get; set; } // actually wants to be a string..?
        [JsonProperty("Shape_Length")]
        public double Length { get; set; }
        [JsonProperty("Shape_Area")]
        public double Area { get; set; }
    }
}