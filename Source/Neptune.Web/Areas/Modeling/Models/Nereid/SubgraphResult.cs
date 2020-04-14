using System.Collections.Generic;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class SubgraphResult
    {
        [JsonProperty("subgraph_nodes")]
        // these will actually just be returned as lists of Nodes, but that's fine--
        // --we can attach the appropriate edges later as needed
        public List<Graph> SubgraphNodes { get; set; }

    }

    public class SolutionSequenceResult
    {
        [JsonProperty("parallel")]
        public List<SolutionSequenceParallel> Parallel { get; set; }
    }

    public class SolutionSequenceParallel
    {
        [JsonProperty("series")]
        public List<SolutionSequenceSeries> Series { get; set; }
    }

    public class SolutionSequenceSeries
    {
        [JsonProperty("nodes")]
        public List<Node> Nodes { get; set; }
    }
}