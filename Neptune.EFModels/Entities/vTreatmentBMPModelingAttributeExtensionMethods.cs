using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class vTreatmentBMPModelingAttributeExtensionMethods
{
    public static vTreatmentBMPModelingAttributeDto AsDto(this vTreatmentBMPModelingAttribute treatmentBMPModelingAttribute)
    {
        var dto = new vTreatmentBMPModelingAttributeDto()
        {
            TreatmentBMPID = treatmentBMPModelingAttribute.TreatmentBMPID,
            UpstreamBMPID = treatmentBMPModelingAttribute.UpstreamBMPID,
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
            RoutingConfiguration = treatmentBMPModelingAttribute.RoutingConfiguration,
            StorageVolumeBelowLowestOutletElevation = treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation,
            SummerHarvestedWaterDemand = treatmentBMPModelingAttribute.SummerHarvestedWaterDemand,
            TimeOfConcentration = treatmentBMPModelingAttribute.TimeOfConcentration,
            DrawdownTimeForDetentionVolume = treatmentBMPModelingAttribute.DrawdownTimeForDetentionVolume,
            TotalEffectiveBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveBMPVolume,
            TotalEffectiveDrywellBMPVolume = treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume,
            TreatmentRate = treatmentBMPModelingAttribute.TreatmentRate,
            UnderlyingHydrologicSoilGroup = treatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroup,
            UnderlyingInfiltrationRate = treatmentBMPModelingAttribute.UnderlyingInfiltrationRate,
            WaterQualityDetentionVolume = treatmentBMPModelingAttribute.WaterQualityDetentionVolume,
            WettedFootprint = treatmentBMPModelingAttribute.WettedFootprint,
            WinterHarvestedWaterDemand = treatmentBMPModelingAttribute.WinterHarvestedWaterDemand,
            MonthsOfOperation = treatmentBMPModelingAttribute.ModeledMonthsOfOperation,
            DryWeatherFlowOverride = treatmentBMPModelingAttribute.DryWeatherFlowOverride
        };
        return dto;
    }
}