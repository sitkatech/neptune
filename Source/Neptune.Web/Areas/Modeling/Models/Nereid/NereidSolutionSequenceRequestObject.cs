using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class NereidSolutionSequenceRequestObject
    {
        [JsonProperty("directed")]
        public bool Directed { get; set; }

        [JsonProperty("edges")]
        public List<Edge> Edges { get; set; }

        public NereidSolutionSequenceRequestObject()
        {
            Directed = true;
        }

        public NereidSolutionSequenceRequestObject(Graph graph)
        {
            Edges = graph.Edges;
            Directed = true;
        }
    }

    public class GenericNeriedResponseWithErrors
    {
        [JsonProperty]
        public List<string> Errors { get; set; }
    }
}