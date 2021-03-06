﻿using Neptune.Web.Common;
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
        public int? RegionalSubbasinID { get; set; }
        [JsonIgnore]
        public Delineation Delineation { get; set; }
        [JsonIgnore]
        public WaterQualityManagementPlanNode WaterQualityManagementPlan { get; set; }
        [JsonIgnore]
        public int? TreatmentBMPID { get; set; }
        [JsonIgnore]
        public JObject Results { get; set; }
        [JsonIgnore]
        public JObject PreviousResults { get; set; }

        public Node(string id)
        {
            ID = id;
        }

        public Node()
        {
        }
    }
}