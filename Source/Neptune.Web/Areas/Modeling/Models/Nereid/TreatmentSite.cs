using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class TreatmentSite
    {
        [JsonProperty("node_id")]
        public string NodeID { get; set; }

        [JsonProperty("facility_type")]
        public string FacilityType { get; set; }

        [JsonProperty("area_pct")]
        public decimal? AreaPercentage { get; set; }

        [JsonProperty("captured_pct")]
        public decimal? CapturedPercentage { get; set; }

        [JsonProperty("retained_pct")]
        public decimal? RetainedPercentage { get; set; }

        [JsonProperty("eliminate_all_dry_weather_flow_override")]
        public bool EliminateAllDryWeatherFlowOverride { get; set; }
    }
}