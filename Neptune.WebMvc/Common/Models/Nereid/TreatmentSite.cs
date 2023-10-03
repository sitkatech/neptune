using System.Text.Json.Serialization;

namespace Neptune.WebMvc.Common.Models.Nereid
{
    public class TreatmentSite
    {
        [JsonPropertyName("node_id")]
        public string NodeID { get; set; }

        [JsonPropertyName("facility_type")]
        public string FacilityType { get; set; }

        [JsonPropertyName("area_pct")]
        public decimal? AreaPercentage { get; set; }

        [JsonPropertyName("captured_pct")]
        public decimal? CapturedPercentage { get; set; }

        [JsonPropertyName("retained_pct")]
        public decimal? RetainedPercentage { get; set; }

        [JsonPropertyName("eliminate_all_dry_weather_flow_override")]
        public bool EliminateAllDryWeatherFlowOverride { get; set; }
    }
}