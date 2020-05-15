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
}
