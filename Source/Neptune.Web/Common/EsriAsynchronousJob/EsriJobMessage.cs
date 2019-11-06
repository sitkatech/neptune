using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Neptune.Web.Common.EsriAsynchronousJob
{
    public class EsriJobMessage
    {
        public string description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EsriJobMessageType type { get; set; }
    }
}