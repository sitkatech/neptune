using System.Text.Json.Serialization;

namespace Neptune.EFModels.Nereid
{
    public class NetworkValidatorResult
    {
        [JsonPropertyName("isvalid")]
        public string IsValid { get; set; }
        [JsonPropertyName("node_cycles")]
        public List<List<string>> NodeCycles { get; set; }
        [JsonPropertyName("edge_cycles")]
        public List<List<string>> EdgeCycles { get; set; }
        [JsonPropertyName("multiple_out_edges")]
        public List<List<string>> MultipleOutEdges { get; set; }
        [JsonPropertyName("duplicate_edges")]
        public List<List<string>> DuplicateEdges { get; set; }
    }
}