using System.Collections.Generic;
using Newtonsoft.Json;

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
        public List<object> PreviousResults { get; set; }
    }
}