using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Neptune.Web.Areas.Modeling.Models.Nereid;

namespace Neptune.Web.Common.Models.Nereid
{
    public class SolutionRequestObject
    {
        [JsonPropertyName("graph")]
        public Graph Graph { get; set; }

        [JsonPropertyName("land_surfaces")]
        public List<LandSurface> LandSurfaces { get; set; }
        
        [JsonPropertyName("treatment_facilities")]
        public List<TreatmentFacility> TreatmentFacilities { get; set; }
        
        [JsonPropertyName("treatment_sites")]
        public List<TreatmentSite> TreatmentSites { get; set; }
        
        [JsonPropertyName("previous_results")]
        public List<JsonObject> PreviousResults { get; set; }
    }
}