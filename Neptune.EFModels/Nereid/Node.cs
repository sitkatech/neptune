using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Neptune.EFModels.Entities;

namespace Neptune.EFModels.Nereid
{
    public class Node
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonIgnore]
        public int? RegionalSubbasinID { get; set; }
        [JsonIgnore]
        public Delineation Delineation { get; set; }
        [JsonIgnore]
        public WaterQualityManagementPlanNode WaterQualityManagementPlan { get; set; }
        [JsonIgnore]
        public int? TreatmentBMPID { get; set; }
        [JsonIgnore]
        public JsonObject? Results { get; set; }
        [JsonIgnore]
        public JsonObject? PreviousResults { get; set; }

        public Node(string id)
        {
            ID = id;
        }

        public Node()
        {
        }
    }
}