using Neptune.Web.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        [JsonIgnore]
        public int? TreatmentBMPID { get; set; }

        public JObject Results { get; set; }

        public Node(string id)
        {
            ID = id;
        }

        public Node()
        {
        }
    }
}