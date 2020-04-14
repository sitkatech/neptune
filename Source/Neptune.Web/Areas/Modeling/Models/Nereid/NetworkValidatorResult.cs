using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class NetworkValidatorResult
    {
        [JsonProperty("isvalid")]
        public string IsValid { get; set; }
        [JsonProperty("node_cycles")]
        public List<List<string>> NodeCycles { get; set; }
        [JsonProperty("edge_cycles")]
        public List<List<string>> EdgeCycles { get; set; }
        [JsonProperty("multiple_out_edges")]
        public List<List<string>> MultipleOutEdges { get; set; }
        [JsonProperty("duplicate_edges")]
        public List<List<string>> DuplicateEdges { get; set; }
    }
}