using System.Text.Json.Serialization;

namespace Neptune.EFModels.Nereid
{
    public class Graph
    {
        [JsonPropertyName("directed")]
        public bool Directed { get; set; }
        [JsonPropertyName("nodes")]
        public List<Node> Nodes { get; set; }
        [JsonPropertyName("edges")]
        public List<Edge> Edges { get; set; }

        public Graph(bool directed, List<Node> nodes, List<Edge> edges)
        {
            Directed = directed;
            Nodes = nodes;
            Edges = edges.GroupBy(x => x.SourceID).Select(x => new Edge(x.Key, x.First().TargetID)).ToList();
        }

        public Graph() { }
    }

    public static class GraphExtensionMethods
    {
        public static Graph GetUpstreamSubgraph(this Graph graph, Node node)
        {
            var edgesToAdd = graph.Edges.Where(x=>x.TargetID == node.ID).ToList();

            var subgraphEdges = new List<Edge>();
            var subgraphNodes = new List<Node>(){node};
            while (edgesToAdd.Any())
            {
                subgraphEdges.AddRange(edgesToAdd);
                var sourceNodeIDs = edgesToAdd.Select(x => x.SourceID).ToList();
                var sourceNodesToAdd = graph.Nodes.Where(x => sourceNodeIDs.Contains(x.ID)).ToList();
                subgraphNodes.AddRange(sourceNodesToAdd);

                edgesToAdd = graph.Edges.Where(x => sourceNodeIDs.Contains(x.TargetID)).ToList();
            }

            return new Graph(true, subgraphNodes, subgraphEdges);
        }
    }
}