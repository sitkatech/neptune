using Neptune.Web.Models;
using Newtonsoft.Json;

namespace Neptune.Web.Areas.Modeling.Models.Nereid
{
    public static class TreatmentFacilityExtensions
    {
        public static TreatmentFacility ToTreatmentFacility(this TreatmentBMP treatmentBMP)
        {
            return new TreatmentFacility
            {
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
                // todo: this bad boi has to be either "summer," "winter," or "both".
                //MonthsofOperation = treatmentBMP.TreatmentBMPModelingAttribute.MonthsofOperation,
                PermanentPoolorWetlandVolume = treatmentBMP.TreatmentBMPModelingAttribute.PermanentPoolorWetlandVolume,
                RoutingConfiguration = treatmentBMP.TreatmentBMPModelingAttribute.RoutingConfiguration,
                StorageVolumeBelowLowestOutletElevation = treatmentBMP.TreatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = treatmentBMP.TreatmentBMPModelingAttribute.SummerHarvestedWaterDemand,
                
                // todo, this appearts to be a lookup table
                TimeofConcentration = treatmentBMP.TreatmentBMPModelingAttribute.TimeOfConcentration,

                // todo: what are this badbois?
                //TotalDrawdownTime = treatmentBMP.TreatmentBMPModelingAttribute.TotalDrawdownTime,
                TotalEffectiveBMPVolume = treatmentBMP.TreatmentBMPModelingAttribute.TotalEffectiveBMPVolume,
                TreatmentRate = treatmentBMP.TreatmentBMPModelingAttribute.TreatmentRate,
                UnderlyingHydrologicSoilGroup = treatmentBMP.TreatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroup,
                UnderlyingInfiltrationRate = treatmentBMP.TreatmentBMPModelingAttribute.UnderlyingInfiltrationRate,
                // todo this bad boi has to be the node id, not the bmp id. Fortunately that's easily constructed
                // UpstreamBMP = treatmentBMP.TreatmentBMPModelingAttribute.UpstreamBMP,
                WaterQualityDetentionVolume = treatmentBMP.TreatmentBMPModelingAttribute.WaterQualityDetentionVolume,
                WettedFootprint = treatmentBMP.TreatmentBMPModelingAttribute.WettedFootprint,
                WinterHarvestedWaterDemand = treatmentBMP.TreatmentBMPModelingAttribute.WinterHarvestedWaterDemand
            };
        }
    }

    public class TreatmentFacility
    {
        [JsonProperty("treatment_rate_cfs")]
        public object AverageDivertedFlowrate { get; set; }

        [JsonProperty("treatment_rate_cfs")]
        public object AverageTreatmentFlowrate { get; set; }

        [JsonProperty("design_capacity_cfs")]
        public object DesignDryWeatherTreatmentCapacity { get; set; }

        [JsonProperty("design_capacity_cfs")]
        public object DesignLowFlowDiversionCapacity { get; set; }

        [JsonProperty("media_filtration_rate_inhr")]
        public object DesignMediaFiltrationRate { get; set; }

        [JsonProperty("pool_drawdown_time_hr")]
        public object DesignResidenceTimeforPermanentPool { get; set; }

        [JsonProperty("offline_diversion_rate_cfs")]
        public object DiversionRate { get; set; }

        [JsonProperty("treatment_drawdown_time_hr")]
        public object DrawdownTimeforWQDetentionVolume { get; set; }

        [JsonProperty("area_sqft")]
        public object EffectiveFootprint { get; set; }

        [JsonProperty("depth_ft")]
        public object EffectiveRetentionDepth { get; set; }

        [JsonProperty("treatment_rate_cfs")]
        public object InfiltrationDischargeRate { get; set; }

        [JsonProperty("area_sqft")]
        public object InfiltrationSurfaceArea { get; set; }

        [JsonProperty("area_sqft")]
        public object MediaBedFootprint { get; set; }

        [JsonProperty("months_operational")]
        public object MonthsofOperation { get; set; }

        [JsonProperty("pool_volume_cuft")]
        public object PermanentPoolorWetlandVolume { get; set; }

        [JsonProperty("is_online")]
        public object RoutingConfiguration { get; set; }

        [JsonProperty("retention_volume_cuft")]
        public object StorageVolumeBelowLowestOutletElevation { get; set; }

        [JsonProperty("summer_demand_cfs")]
        public object SummerHarvestedWaterDemand { get; set; }

        [JsonProperty("tributary_area_tc_min")]
        public object TimeofConcentration { get; set; }

        [JsonProperty("total_drawdown_time_hr")]
        public object TotalDrawdownTime { get; set; }

        [JsonProperty("total_volume_cuft")]
        public object TotalEffectiveBMPVolume { get; set; }

        [JsonProperty("treatment_rate_cfs")]
        public object TreatmentRate { get; set; }

        [JsonProperty("hsg")]
        public object UnderlyingHydrologicSoilGroup { get; set; }

        [JsonProperty("inf_rate_inhr")]
        public object UnderlyingInfiltrationRate { get; set; }

        [JsonProperty("upstream_node_id")]
        public object UpstreamBMP { get; set; }

        [JsonProperty("treatment_volume_cuft")]
        public object WaterQualityDetentionVolume { get; set; }

        [JsonProperty("area_sqft")]
        public object WettedFootprint { get; set; }

        [JsonProperty("winter_demand_cfs")]
        public object WinterHarvestedWaterDemand { get; set; }
    }
}