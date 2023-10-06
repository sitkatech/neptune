using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Neptune.EFModels.Nereid
{
    public class NereidResult<T>
    {
        [JsonPropertyName("task_id")]
        public string TaskID { get; set; }
        [JsonPropertyName("status")]
        public NereidJobStatus Status { get; set; }
        [JsonPropertyName("data")]
        public T Data { get; set; }
        [JsonPropertyName("result_route")]
        public string ResultRoute { get; set; }

        [JsonPropertyName("detail")]
        public JsonArray Detail { get; set; }
    }
}