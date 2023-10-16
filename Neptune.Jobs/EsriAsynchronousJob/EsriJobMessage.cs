using System.Text.Json.Serialization;

namespace Neptune.Jobs.EsriAsynchronousJob
{
    public class EsriJobMessage
    {
        public string description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EsriJobMessageType type { get; set; }
    }
}