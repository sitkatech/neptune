using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities;

public static partial class TreatmentBMPTypeExtensionMethods
{
    public static TreatmentBMPTypeDisplayDto AsDisplayDto(this TreatmentBMPType treatmentBMPType)
    {
        var treatmentBMPTypeDisplayDto = new TreatmentBMPTypeDisplayDto()
        {
            TreatmentBMPTypeID = treatmentBMPType.TreatmentBMPTypeID,
            TreatmentBMPTypeName = treatmentBMPType.TreatmentBMPTypeName,
        };
        return treatmentBMPTypeDisplayDto;
    }

    public static bool HasMissingModelingAttributes(this TreatmentBMPType treatmentBMPType, TreatmentBMPModelingAttribute? treatmentBMPModelingAttribute)
    {
        if (!treatmentBMPType.IsAnalyzedInModelingModule)
        {
            return false;
        }

        var treatmentBMPModelingTypeEnum = treatmentBMPType.TreatmentBMPModelingType.ToEnum;
        if (treatmentBMPModelingAttribute != null)
        {
            var hasNoRoutingConfigurationAndOffline = !treatmentBMPModelingAttribute.RoutingConfigurationID.HasValue ||
                                                      treatmentBMPModelingAttribute is { RoutingConfigurationID: (int)RoutingConfigurationEnum.Offline, DiversionRate: null };
            switch (treatmentBMPModelingTypeEnum)
            {
                case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation.HasValue ||
                           !treatmentBMPModelingAttribute.MediaBedFootprint.HasValue ||
                           !treatmentBMPModelingAttribute.DesignMediaFiltrationRate.HasValue;
                case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                case TreatmentBMPModelingTypeEnum.PermeablePavement:
                case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.InfiltrationSurfaceArea.HasValue ||
                           !treatmentBMPModelingAttribute.UnderlyingInfiltrationRate.HasValue;
                case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                case TreatmentBMPModelingTypeEnum.SandFilters:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.MediaBedFootprint.HasValue ||
                           !treatmentBMPModelingAttribute.DesignMediaFiltrationRate.HasValue;
                case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.WinterHarvestedWaterDemand.HasValue ||
                           !treatmentBMPModelingAttribute.SummerHarvestedWaterDemand.HasValue;
                case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.PermanentPoolOrWetlandVolume.HasValue ||
                           !treatmentBMPModelingAttribute.WaterQualityDetentionVolume.HasValue;
                case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.StorageVolumeBelowLowestOutletElevation.HasValue ||
                           !treatmentBMPModelingAttribute.EffectiveFootprint.HasValue ||
                           !treatmentBMPModelingAttribute.DrawdownTimeForWQDetentionVolume.HasValue;
                case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                    return !treatmentBMPModelingAttribute.DesignDryWeatherTreatmentCapacity.HasValue &&
                           !treatmentBMPModelingAttribute.AverageTreatmentFlowrate.HasValue;
                case TreatmentBMPModelingTypeEnum.Drywell:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TotalEffectiveDrywellBMPVolume.HasValue ||
                           !treatmentBMPModelingAttribute.InfiltrationDischargeRate.HasValue;
                case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                    return !treatmentBMPModelingAttribute.TreatmentRate.HasValue;
                case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                    return !treatmentBMPModelingAttribute.DesignLowFlowDiversionCapacity.HasValue &&
                           !treatmentBMPModelingAttribute.AverageDivertedFlowrate.HasValue;
                case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                    return hasNoRoutingConfigurationAndOffline ||
                           !treatmentBMPModelingAttribute.TreatmentRate.HasValue ||
                           !treatmentBMPModelingAttribute.WettedFootprint.HasValue ||
                           !treatmentBMPModelingAttribute.EffectiveRetentionDepth.HasValue;
                default:
                    throw new ArgumentOutOfRangeException(
                        $"Unknown TreatmentBMPModelingTypeEnum + {treatmentBMPModelingTypeEnum}");
            }
        }

        return true;
    }
}