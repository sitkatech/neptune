//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPModelingAttribute]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPModelingAttributeExtensionMethods
    {
        public static TreatmentBMPModelingAttributeSimpleDto AsSimpleDto(this TreatmentBMPModelingAttribute treatmentBMPModelingAttribute)
        {
            var dto = new TreatmentBMPModelingAttributeSimpleDto()
            {
                TreatmentBMPModelingAttributeID = treatmentBMPModelingAttribute.TreatmentBMPModelingAttributeID,
                TreatmentBMPID = treatmentBMPModelingAttribute.TreatmentBMPID,
                UpstreamTreatmentBMPID = treatmentBMPModelingAttribute.UpstreamTreatmentBMPID,
                AverageDivertedFlowrate = treatmentBMPModelingAttribute.AverageDivertedFlowrate,
                AverageTreatmentFlowrate = treatmentBMPModelingAttribute.AverageTreatmentFlowrate,
                DesignDryWeatherTreatmentCapacity = treatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity,
                DesignLowFlowDiversionCapacity = treatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity,
                DesignMediaFiltrationRate = treatmentBMPModelingAttribute.DesignMediaFiltrationRate,
                DiversionRate = treatmentBMPModelingAttribute.DiversionRate,
                DrawdownTimeForWQDetentionVolume = treatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume,
                EffectiveFootprint = treatmentBMPModelingAttribute.EffectiveFootprint,
                EffectiveRetentionDepth = treatmentBMPModelingAttribute.EffectiveRetentionDepth,
                InfiltrationDischargeRate = treatmentBMPModelingAttribute.InfiltrationDischargeRate,
                InfiltrationSurfaceArea = treatmentBMPModelingAttribute.InfiltrationSurfaceArea,
                MediaBedFootprint = treatmentBMPModelingAttribute.MediaBedFootprint,
                PermanentPoolOrWetlandVolume = treatmentBMPModelingAttribute.PermanentPoolOrWetlandVolume,
                RoutingConfigurationID = treatmentBMPModelingAttribute.RoutingConfigurationID,
                StorageVolumeBelowLowestOutletElevation = treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation,
                SummerHarvestedWaterDemand = treatmentBMPModelingAttribute.SummerHarvestedWaterDemand,
                TimeOfConcentrationID = treatmentBMPModelingAttribute.TimeOfConcentrationID,
                DrawdownTimeForDetentionVolume = treatmentBMPModelingAttribute.DrawdownTimeForDetentionVolume,
                TotalEffectiveBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveBMPVolume,
                TotalEffectiveDrywellBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume,
                TreatmentRate = treatmentBMPModelingAttribute.TreatmentRate,
                UnderlyingHydrologicSoilGroupID = treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroupID,
                UnderlyingInfiltrationRate = treatmentBMPModelingAttribute.UnderlyingInfiltrationRate,
                WaterQualityDetentionVolume = treatmentBMPModelingAttribute.WaterQualityDetentionVolume,
                WettedFootprint = treatmentBMPModelingAttribute.WettedFootprint,
                WinterHarvestedWaterDemand = treatmentBMPModelingAttribute.WinterHarvestedWaterDemand,
                MonthsOfOperationID = treatmentBMPModelingAttribute.MonthsOfOperationID,
                DryWeatherFlowOverrideID = treatmentBMPModelingAttribute.DryWeatherFlowOverrideID
            };
            return dto;
        }
    }
}