using System.Text.Json.Serialization;

namespace Neptune.EFModels.Nereid
{
    public class TreatmentFacility
    {
        [JsonPropertyName("node_id")]
        public string NodeID { get; set; }

        [JsonPropertyName("facility_type")]
        public string FacilityType { get; set; }
        
        [JsonPropertyName("ref_data_key")]
        public string ReferenceDataKey { get; set; }
        
        [JsonPropertyName("media_filtration_rate_inhr")]
        public double? DesignMediaFiltrationRate { get; set; }

        [JsonPropertyName("offline_diversion_rate_cfs")]
        public double? DiversionRate { get; set; }

        [JsonPropertyName("treatment_drawdown_time_hr")]
        public double? DrawdownTimeforWQDetentionVolume { get; set; }

        [JsonPropertyName("depth_ft")]
        public double? EffectiveRetentionDepth { get; set; }

        [JsonPropertyName("months_operational")]
        public string MonthsOfOperation { get; set; }

        [JsonPropertyName("pool_volume_cuft")]
        public double? PermanentPoolorWetlandVolume { get; set; }

        [JsonPropertyName("is_online")]
        public bool RoutingConfiguration { get; set; }

        [JsonPropertyName("retention_volume_cuft")]
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }

        [JsonPropertyName("summer_demand_cfs")]
        public double? SummerHarvestedWaterDemand { get; set; }

        [JsonPropertyName("tributary_area_tc_min")]
        public string TimeOfConcentration { get; set; }

        [JsonPropertyName("total_drawdown_time_hr")]
        public double? TotalDrawdownTime { get; set; }

        [JsonPropertyName("total_volume_cuft")]
        public double? TotalEffectiveBMPVolume { get; set; }

        [JsonPropertyName("treatment_rate_cfs")]
        public double? TreatmentRate { get; set; }

        [JsonPropertyName("hsg")]
        public string UnderlyingHydrologicSoilGroup { get; set; }

        [JsonPropertyName("inf_rate_inhr")]
        public double? UnderlyingInfiltrationRate { get; set; }

        [JsonPropertyName("upstream_node_id")]
        public string UpstreamBMP { get; set; }

        [JsonPropertyName("treatment_volume_cuft")]
        public double? WaterQualityDetentionVolume { get; set; }

        [JsonPropertyName("winter_demand_cfs")]
        public double? WinterHarvestedWaterDemand { get; set; }

        [JsonPropertyName("design_storm_depth_inches")]
        public double? DesignStormwaterDepth { get; set; }

        [JsonPropertyName("area_sqft")]
        public double? Area { get; set; }

        [JsonPropertyName("design_capacity_cfs")]
        public double? DesignCapacity { get; set; }

        [JsonPropertyName("eliminate_all_dry_weather_flow_override")]
        public bool EliminateAllDryWeatherFlowOverride { get; set; }
    }
}