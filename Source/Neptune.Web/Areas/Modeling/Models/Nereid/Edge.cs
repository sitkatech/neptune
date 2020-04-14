using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class Edge
    {
        [JsonProperty("source")]
        public string SourceID { get; set; }
        [JsonProperty("target")]
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