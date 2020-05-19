using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class SolutionRequestObject
    {
        [JsonProperty("graph")]
        public Graph Graph { get; set; }

        [JsonProperty("land_surfaces")]
        public List<LandSurface> LandSurfaces { get; set; }
        
        [JsonProperty("treatment_facilities")]
        public List<TreatmentFacility> TreatmentFacilities { get; set; }
        
        [JsonProperty("treatment_sites")]
        public List<TreatmentSite> TreatmentSites { get; set; }
        
        [JsonProperty("previous_results")]
        public List<JObject> PreviousResults { get; set; }
    }

    public class SolutionResponseObject : GenericNeriedResponse
    {
        [JsonProperty("results")]
        public List<JObject> Results { get; set; }

        [JsonProperty("leaf_results")]
        public List<JObject> LeafResults { get; set; }

        [JsonProperty("previous_results_keys")]
        public List<string> PreviousResultsKeys { get; set; }


    }
}