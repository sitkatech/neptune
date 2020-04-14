using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class Graph
    {
        [JsonProperty("directed")]
        public bool Directed { get; set; }
        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; }
        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }

        public Graph(bool directed, List<Node> nodes, List<Edge> edges)
        {
            Directed = directed;
            Nodes = nodes;
            Edges = edges;
        }

        public Graph() { }
    }
}