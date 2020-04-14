using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class NereidResult<T>
    {
        [JsonProperty("task_id")]
        public string TaskID { get; set; }
        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public NereidJobStatus Status { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
        [JsonProperty("result_route")]
        public string ResultRoute { get; set; }
    }
}