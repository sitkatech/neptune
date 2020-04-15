using System.Collections.Generic;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public class TreatmentFacilityTable
    {
        [JsonProperty("treatment_facilities")]
        public List<TreatmentFacility> TreatmentFacilities { get; set; }
    }
    public class TreatmentFacility
    {
        [JsonProperty("node_id")]
        public string NodeID { get; set; }

        [JsonProperty("facility_type")]
        public string FacilityType { get; set; }
        
        [JsonProperty("ref_data_key")]
        public string ReferenceDataKey { get; set; }

        [JsonProperty("treatment_rate_cfs")]
        public double? AverageDivertedFlowrate { get; set; }

        // todo: developer of Nereid kinda forgot that a JSON object can have at most one property with a given name
        // have to get him to update this to use different names
        //[JsonProperty("treatment_rate_cfs")]
        [JsonIgnore]
        public double? AverageTreatmentFlowrate { get; set; }

        [JsonProperty("design_capacity_cfs")]
        public double? DesignDryWeatherTreatmentCapacity { get; set; }

//        [JsonProperty("design_capacity_cfs")]
        [JsonIgnore]
        public double? DesignLowFlowDiversionCapacity { get; set; }

        [JsonProperty("media_filtration_rate_inhr")]
        public double? DesignMediaFiltrationRate { get; set; }

        [JsonProperty("pool_drawdown_time_hr")]
        public double? DesignResidenceTimeforPermanentPool { get; set; }

        [JsonProperty("offline_diversion_rate_cfs")]
        public double? DiversionRate { get; set; }

        [JsonProperty("treatment_drawdown_time_hr")]
        public double? DrawdownTimeforWQDetentionVolume { get; set; }

        [JsonProperty("area_sqft")]
        public double? EffectiveFootprint { get; set; }

        [JsonProperty("depth_ft")]
        public double? EffectiveRetentionDepth { get; set; }

        //[JsonProperty("treatment_rate_cfs")]
        [JsonIgnore]
        public double? InfiltrationDischargeRate { get; set; }

        //[JsonProperty("area_sqft")]
        [JsonIgnore]
        public double? InfiltrationSurfaceArea { get; set; }

        //[JsonProperty("area_sqft")]
        [JsonIgnore]
        public double? MediaBedFootprint { get; set; }

        [JsonProperty("months_operational")]
        public string MonthsOfOperation { get; set; }

        [JsonProperty("pool_volume_cuft")]
        public double? PermanentPoolorWetlandVolume { get; set; }

        [JsonProperty("is_online")]
        public bool RoutingConfiguration { get; set; }

        [JsonProperty("retention_volume_cuft")]
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }

        [JsonProperty("summer_demand_cfs")]
        public double? SummerHarvestedWaterDemand { get; set; }

        [JsonProperty("tributary_area_tc_min")]
        public string TimeOfConcentration { get; set; }

        [JsonProperty("total_drawdown_time_hr")]
        public double? TotalDrawdownTime { get; set; }

        [JsonProperty("total_volume_cuft")]
        public double? TotalEffectiveBMPVolume { get; set; }

        //[JsonProperty("treatment_rate_cfs")]
        [JsonIgnore]
        public double? TreatmentRate { get; set; }

        [JsonProperty("hsg")]
        public string UnderlyingHydrologicSoilGroup { get; set; }

        [JsonProperty("inf_rate_inhr")]
        public double? UnderlyingInfiltrationRate { get; set; }

        [JsonProperty("upstream_node_id")]
        public string UpstreamBMP { get; set; }

        [JsonProperty("treatment_volume_cuft")]
        public double? WaterQualityDetentionVolume { get; set; }

        //[JsonProperty("area_sqft")]
        [JsonIgnore]
        public double? WettedFootprint { get; set; }

        [JsonProperty("winter_demand_cfs")]
        public double? WinterHarvestedWaterDemand { get; set; }

        [JsonProperty("design_storm_depth_inches")]
        public double? DesignStormwaterDepth { get; set; }
    }

    public static class TreatmentFacilityExtensions
    {
        public static TreatmentFacility ToTreatmentFacility(this TreatmentBMP treatmentBMP)
        {
            var treatmentBMPNodeID = NereidUtilities.TreatmentBMPNodeID(treatmentBMP);
            var lspcBasinKey = treatmentBMP.LSPCBasin?.LSPCBasinKey.ToString();
            var isFullyParameterized = treatmentBMP.IsFullyParameterized();
            
            

            if (!isFullyParameterized)
            {
                return new TreatmentFacility
                {
                    NodeID = treatmentBMPNodeID,
                    FacilityType = "NoTreatment",
                    ReferenceDataKey = lspcBasinKey
                };
            }

            var treatmentFacility = new TreatmentFacility
            {
                NodeID = treatmentBMPNodeID,
                FacilityType = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType.TreatmentBMPModelingTypeName,
                ReferenceDataKey = lspcBasinKey,
                DesignStormwaterDepth = treatmentBMP.PrecipitationZone.DesignStormwaterDepthInInches,
                AverageDivertedFlowrate = treatmentBMP.TreatmentBMPModelingAttribute.AverageDivertedFlowrate,
                AverageTreatmentFlowrate = treatmentBMP.TreatmentBMPModelingAttribute.AverageTreatmentFlowrate,
                DesignDryWeatherTreatmentCapacity = treatmentBMP.TreatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity,
                DesignLowFlowDiversionCapacity = treatmentBMP.TreatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity,
                DesignMediaFiltrationRate = treatmentBMP.TreatmentBMPModelingAttribute.DesignMediaFiltrationRate,
                DesignResidenceTimeforPermanentPool = treatmentBMP.TreatmentBMPModelingAttribute.DesignResidenceTimeforPermanentPool,
                DiversionRate = treatmentBMP.TreatmentBMPModelingAttribute.DiversionRate,
                DrawdownTimeforWQDetentionVolume = treatmentBMP.TreatmentBMPModelingAttribute.DrawdownTimeforWQDetentionVolume,
                EffectiveFootprint = treatmentBMP.TreatmentBMPModelingAttribute.EffectiveFootprint,
                EffectiveRetentionDepth = treatmentBMP.TreatmentBMPModelingAttribute.EffectiveRetentionDepth,
                InfiltrationDischargeRate = treatmentBMP.TreatmentBMPModelingAttribute.InfiltrationDischargeRate,
                InfiltrationSurfaceArea = treatmentBMP.TreatmentBMPModelingAttribute.InfiltrationSurfaceArea,
                MediaBedFootprint = treatmentBMP.TreatmentBMPModelingAttribute.MediaBedFootprint,
                // todo: this bad boi has to be either "summer," "winter," or "both", which change will be made soon.
                MonthsOfOperation = "jan",
                PermanentPoolorWetlandVolume = treatmentBMP.TreatmentBMPModelingAttribute.PermanentPoolorWetlandVolume,
                RoutingConfiguration = treatmentBMP.TreatmentBMPModelingAttribute.RoutingConfigurationID == RoutingConfiguration.Online.RoutingConfigurationID ? true : false,
                StorageVolumeBelowLowestOutletElevation = treatmentBMP.TreatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = treatmentBMP.TreatmentBMPModelingAttribute.SummerHarvestedWaterDemand,
                
                TimeOfConcentration = treatmentBMP.TreatmentBMPModelingAttribute.TimeOfConcentration?.TimeOfConcentrationDisplayName,

                TotalDrawdownTime = treatmentBMP.TreatmentBMPModelingAttribute.DrawdownTimeForDetentionVolume,
                TotalEffectiveBMPVolume = treatmentBMP.TreatmentBMPModelingAttribute.TotalEffectiveBMPVolume,
                TreatmentRate = treatmentBMP.TreatmentBMPModelingAttribute.TreatmentRate,
                UnderlyingHydrologicSoilGroup = treatmentBMP.TreatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroup?.UnderlyingHydrologicSoilGroupDisplayName,
                UnderlyingInfiltrationRate = treatmentBMP.TreatmentBMPModelingAttribute.UnderlyingInfiltrationRate,
                UpstreamBMP = treatmentBMP.TreatmentBMPModelingAttribute.UpstreamTreatmentBMPID.HasValue ? NereidUtilities.TreatmentBMPNodeID(treatmentBMP.TreatmentBMPModelingAttribute.UpstreamTreatmentBMPID.Value) : null,
                WaterQualityDetentionVolume = treatmentBMP.TreatmentBMPModelingAttribute.WaterQualityDetentionVolume,
                WettedFootprint = treatmentBMP.TreatmentBMPModelingAttribute.WettedFootprint,
                WinterHarvestedWaterDemand = treatmentBMP.TreatmentBMPModelingAttribute.WinterHarvestedWaterDemand
            };
            return treatmentFacility;
        }
    }
}