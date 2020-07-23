using Neptune.Web.Common;
using Neptune.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        
        [JsonProperty("media_filtration_rate_inhr")]
        public double? DesignMediaFiltrationRate { get; set; }

        [JsonProperty("pool_drawdown_time_hr")]
        public double? DesignResidenceTimeforPermanentPool { get; set; }

        [JsonProperty("offline_diversion_rate_cfs")]
        public double? DiversionRate { get; set; }

        [JsonProperty("treatment_drawdown_time_hr")]
        public double? DrawdownTimeforWQDetentionVolume { get; set; }

        [JsonProperty("depth_ft")]
        public double? EffectiveRetentionDepth { get; set; }

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

        [JsonProperty("treatment_rate_cfs")]
        public double? TreatmentRate { get; set; }

        [JsonProperty("hsg")]
        public string UnderlyingHydrologicSoilGroup { get; set; }

        [JsonProperty("inf_rate_inhr")]
        public double? UnderlyingInfiltrationRate { get; set; }

        [JsonProperty("upstream_node_id")]
        public string UpstreamBMP { get; set; }

        [JsonProperty("treatment_volume_cuft")]
        public double? WaterQualityDetentionVolume { get; set; }

        [JsonProperty("winter_demand_cfs")]
        public double? WinterHarvestedWaterDemand { get; set; }

        [JsonProperty("design_storm_depth_inches")]
        public double? DesignStormwaterDepth { get; set; }

        [JsonProperty("area_sqft")]
        public double? Area { get; set; }

        [JsonProperty("design_capacity_cfs")]
        public double? DesignCapacity { get; set; }
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
                    ReferenceDataKey = lspcBasinKey,
                    DesignStormwaterDepth = treatmentBMP.PrecipitationZone.DesignStormwaterDepthInInches,
                };
            }

            double? treatmentRate = null;
            
            var modelingAttribute = treatmentBMP.TreatmentBMPModelingAttribute;

            // treatment rate is an alias for four different fields, so we need to pick the one that's not null
            if (modelingAttribute.InfiltrationDischargeRate != null)
            {
                treatmentRate = modelingAttribute.InfiltrationDischargeRate;
            }
            else if (modelingAttribute.TreatmentRate != null)
            {
                treatmentRate = modelingAttribute.TreatmentRate;
            }
            else if (modelingAttribute.AverageDivertedFlowrate != null)
            {
                treatmentRate = modelingAttribute.AverageDivertedFlowrate;
            }
            else if (modelingAttribute.AverageTreatmentFlowrate != null)
            {
                treatmentRate = modelingAttribute.AverageTreatmentFlowrate;
            }

            double? area = null;

            // area_sqft is an alias for four different fields, so we need to take the one that's not null
            if (modelingAttribute.EffectiveFootprint != null)
            {
                area = modelingAttribute.EffectiveFootprint;
            }
            else if (modelingAttribute.InfiltrationSurfaceArea != null)
            {
                area = modelingAttribute.InfiltrationSurfaceArea;
            }
            else if (modelingAttribute.MediaBedFootprint != null)
            {
                area = modelingAttribute.MediaBedFootprint;
            }
            else if (modelingAttribute.WettedFootprint != null)
            {
                area = modelingAttribute.WettedFootprint;
            }

            double? designCapacity = null;

            if (modelingAttribute.DesignDryWeatherTreatmentCapacity != null)
            {
                designCapacity = modelingAttribute.DesignDryWeatherTreatmentCapacity;
            }
            else if (modelingAttribute.DesignLowFlowDiversionCapacity != null)
            {
                designCapacity = modelingAttribute.DesignLowFlowDiversionCapacity;
            }

            if (designCapacity == null)
            {
                designCapacity = treatmentRate;
            }
            else if (treatmentRate == null)
            {
                treatmentRate = designCapacity;
            }

            // null-coalescence here represents default values.
            // It's generally an anti-pattern for an API to require its clients to insert default values,
            // since the API should be written to replace nulls/absent values with defaults,
            // but it's so few that it's not worth a revision to the modeling engine at this time (4/16/2020)
            var treatmentFacility = new TreatmentFacility
            {
                NodeID = treatmentBMPNodeID,
                FacilityType = treatmentBMP.TreatmentBMPType.TreatmentBMPModelingType.TreatmentBMPModelingTypeName,
                ReferenceDataKey = lspcBasinKey,
                DesignStormwaterDepth = treatmentBMP.PrecipitationZone.DesignStormwaterDepthInInches,
                DesignCapacity = designCapacity,
                DesignMediaFiltrationRate = modelingAttribute.DesignMediaFiltrationRate,
                DesignResidenceTimeforPermanentPool = modelingAttribute.DesignResidenceTimeforPermanentPool ?? double.PositiveInfinity,
                DiversionRate = modelingAttribute.DiversionRate,
                DrawdownTimeforWQDetentionVolume = modelingAttribute.DrawdownTimeforWQDetentionVolume,
                Area = area,
                EffectiveRetentionDepth = modelingAttribute.EffectiveRetentionDepth,
                MonthsOfOperation = modelingAttribute.MonthsOfOperation?.MonthsOfOperationNereidAlias ?? MonthsOfOperation.Both.MonthsOfOperationNereidAlias,
                PermanentPoolorWetlandVolume = modelingAttribute.PermanentPoolorWetlandVolume,
                RoutingConfiguration = modelingAttribute.RoutingConfigurationID == RoutingConfiguration.Online.RoutingConfigurationID,
                StorageVolumeBelowLowestOutletElevation = modelingAttribute.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = modelingAttribute.SummerHarvestedWaterDemand,
                TimeOfConcentration = modelingAttribute.TimeOfConcentration?.TimeOfConcentrationDisplayName ?? TimeOfConcentration.FiveMinutes.TimeOfConcentrationDisplayName,
                TotalDrawdownTime = modelingAttribute.DrawdownTimeforWQDetentionVolume,
                TotalEffectiveBMPVolume = modelingAttribute.TotalEffectiveBMPVolume,
                TreatmentRate = treatmentRate,
                UnderlyingHydrologicSoilGroup = modelingAttribute.UnderlyingHydrologicSoilGroup?.UnderlyingHydrologicSoilGroupDisplayName.ToLower() ?? UnderlyingHydrologicSoilGroup.D.UnderlyingHydrologicSoilGroupDisplayName.ToLower(),
                UnderlyingInfiltrationRate = modelingAttribute.UnderlyingInfiltrationRate,
                UpstreamBMP = modelingAttribute.UpstreamTreatmentBMPID.HasValue ? NereidUtilities.TreatmentBMPNodeID(modelingAttribute.UpstreamTreatmentBMPID.Value) : null,
                WaterQualityDetentionVolume = modelingAttribute.WaterQualityDetentionVolume,
                WinterHarvestedWaterDemand = modelingAttribute.WinterHarvestedWaterDemand
            };
            return treatmentFacility;
        }
    }
}