using System.Collections.Generic;
using Neptune.Web.Common.Models.Nereid;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class SolutionSequenceResult
    {
        [JsonProperty("solution_sequence")]
        public SolutionSequenceInternal SolutionSequence { get; set; }
    }

    public class SolutionSequenceInternal
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