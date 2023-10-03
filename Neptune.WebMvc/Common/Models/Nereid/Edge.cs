using System.Text.Json.Serialization;

namespace Neptune.WebMvc.Common.Models.Nereid
{
    public class Edge
    {
        [JsonPropertyName("source")]
        public string SourceID { get; set; }
        [JsonPropertyName("target")]
        public string TargetID { get; set; }

        public Edge(string sourceID, string targetID)
        {
            SourceID = sourceID;
            TargetID = targetID;
        }

        public Edge()
        {
        }
    }
}