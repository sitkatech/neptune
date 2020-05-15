using Neptune.Web.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class Node
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonIgnore]
        public RegionalSubbasin RegionalSubbasin { get; set; }
        [JsonIgnore]
        public Delineation Delineation { get; set; }
        [JsonIgnore]
        public WaterQualityManagementPlanNode WaterQualityManagementPlan { get; set; }

        public int? TreatmentBMPID { get; set; }

        public Node(string id)
        {
            ID = id;
        }

        public Node()
        {
        }
    }
}