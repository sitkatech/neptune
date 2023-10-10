using System.Text.Json.Serialization;

namespace Neptune.API.Models.EsriAsynchronousJob
{
    public class EsriJobMessage
    {
        public string description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EsriJobMessageType type { get; set; }
    }
}