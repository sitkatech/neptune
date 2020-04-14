using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class NereidSubgraphRequestObject
    {
        [JsonProperty("graph")]
        public Graph Graph { get; set; }
        [JsonProperty("nodes")]
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