using System.Text.Json.Serialization;

namespace Neptune.WebMvc.Common.Models.Nereid
{
    public class NereidSubgraphRequestObject
    {
        [JsonPropertyName("graph")]
        public Graph Graph { get; set; }
        [JsonPropertyName("nodes")]
        public List<Node> Nodes { get; set; }

        public NereidSubgraphRequestObject()
        {
        }

        public NereidSubgraphRequestObject(Graph graph, List<Node> nodes)
        {
            Graph = graph;
            Nodes = nodes;
        }
    }
}