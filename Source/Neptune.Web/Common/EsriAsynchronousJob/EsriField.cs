using Newtonsoft.Json;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class EsriField
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("alias")]
        public string Alias { get; set; }
        [JsonProperty("length")]
        public int Length { get; set; }
    }
}