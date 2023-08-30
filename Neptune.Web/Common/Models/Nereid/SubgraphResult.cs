using System.Text.Json.Serialization;

namespace Neptune.Web.Common.Models.Nereid
{
    public class SubgraphResult
    {
        [JsonPropertyName("subgraph_nodes")]
        // these will actually just be returned as lists of Nodes, but that's fine--
        // --we can attach the appropriate edges later as needed
        public List<Graph> SubgraphNodes { get; set; }

    }
}
