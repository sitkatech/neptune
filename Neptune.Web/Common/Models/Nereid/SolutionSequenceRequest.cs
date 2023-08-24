using System.Text.Json.Serialization;

namespace Neptune.Web.Common.Models.Nereid
{
    public class SolutionSequenceRequest
    {
        [JsonPropertyName("directed")]
        public bool Directed { get; set; }

        [JsonPropertyName("edges")]
        public List<Edge> Edges { get; set; }

        public SolutionSequenceRequest()
        {
            Directed = true;
        }

        public SolutionSequenceRequest(Graph graph)
        {
            Edges = graph.Edges.GroupBy(x => x.SourceID).Select(x => new Edge(x.Key, x.First().TargetID)).ToList();
            Directed = true;
        }
    }
}