using System.Text.Json.Serialization;

namespace Neptune.EFModels.Nereid
{
    public class SolutionSequenceResult
    {
        [JsonPropertyName("solution_sequence")]
        public SolutionSequenceInternal SolutionSequence { get; set; }
    }

    public class SolutionSequenceInternal
    {
        [JsonPropertyName("parallel")]
        public List<SolutionSequenceParallel> Parallel { get; set; }
    }

    public class SolutionSequenceParallel
    {
        [JsonPropertyName("series")]
        public List<SolutionSequenceSeries> Series { get; set; }
    }

    public class SolutionSequenceSeries
    {
        [JsonPropertyName("nodes")]
        public List<Node> Nodes { get; set; }
    }
}